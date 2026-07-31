# SessionPool Exception Handling and Health Monitoring

## Overview

The Apache IoTDB C# client library provides comprehensive exception handling and health monitoring capabilities for SessionPool operations. This document explains how to handle pool depletion scenarios, monitor pool health, and implement recovery strategies.

## SessionPoolDepletedException

### Description

`SessionPoolDepletedException` is a specialized exception thrown when the SessionPool cannot provide a client connection. This indicates that:

- All clients in the pool are currently in use, OR
- Client connections have failed and reconnection attempts were unsuccessful, OR
- The pool wait timeout has been exceeded

### Exception Properties

The exception provides detailed diagnostic information through the following properties:

| Property              | Type   | Description                                                                |
| --------------------- | ------ | -------------------------------------------------------------------------- |
| `DepletionReason`     | string | A human-readable description of why the pool was depleted                  |
| `AvailableClients`    | int    | Number of currently available clients in the pool at the time of exception |
| `TotalPoolSize`       | int    | The total configured size of the session pool                              |
| `FailedReconnections` | int    | Number of failed reconnection attempts since the pool was opened           |

### Example Usage

```csharp
using Apache.IoTDB;
using System;

try
{
    var sessionPool = new SessionPool.Builder()
        .SetHost("127.0.0.1")
        .SetPort(6667)
        .SetPoolSize(4)
        .Build();

    await sessionPool.Open();

    // Perform operations...
    await sessionPool.InsertRecordAsync("root.sg.d1", record);
}
catch (SessionPoolDepletedException ex)
{
    Console.WriteLine($"Pool depleted: {ex.DepletionReason}");
    Console.WriteLine($"Available clients: {ex.AvailableClients}/{ex.TotalPoolSize}");
    Console.WriteLine($"Failed reconnections: {ex.FailedReconnections}");

    // Implement recovery strategy (see below)
}
```

## `IsOpen()` is a lifecycle flag, not a health check

`SessionPool.IsOpen()` reports whether **you** have opened the pool and not yet closed it. It is not a
connectivity probe:

- It becomes `true` after a successful `Open()` and only returns to `false` when you call `Close()`.
- The client runs no heartbeat, so a server that goes down does **not** flip it back to `false`.
  Reconnection happens lazily, on the next operation.

This means the following common guard never re-opens the pool, because the flag stays `true` forever:

```csharp
// Anti-pattern: this short-circuits even while every connection is dead
if (_pool != null && _pool.IsOpen()) return;
```

To reason about actual availability, use the health metrics below, or simply let an operation throw
`SessionPoolDepletedException` and handle it.

## Pool Wait Timeout

Two independent timeouts govern a pool operation:

| Setting                                   | Unit | Default | Controls                                                                  |
| ----------------------------------------- | ---- | ------- | ------------------------------------------------------------------------- |
| `SetConnectionTimeoutInMs(int)`           | ms   | 500     | Socket-level send/receive timeout of an individual connection             |
| `SetPoolWaitTimeoutInMs(int)`             | ms   | 10000   | How long an operation waits for a free client before the pool gives up    |

```csharp
var sessionPool = new SessionPool.Builder()
    .SetHost("127.0.0.1")
    .SetPort(6667)
    .SetPoolSize(8)
    .SetConnectionTimeoutInMs(500)   // socket timeout
    .SetPoolWaitTimeoutInMs(10_000)  // give up after 10s of waiting for a free client
    .Build();
```

When the wait budget is exhausted, the operation throws `SessionPoolDepletedException` with the reason
`Connection pool is empty and wait time out(...ms)`. Raise `SetPoolWaitTimeoutInMs` if your workload
legitimately queues behind long operations; lower it if you would rather fail fast and retry.

> **Note:** before this setting existed, the wait budget was derived from the connection timeout and then
> misinterpreted as seconds, which turned the 500 ms default into a ~41 minute block. If you are upgrading
> from an older version and relied on that (unintended) long wait, set `SetPoolWaitTimeoutInMs` explicitly.

## Pool Health Metrics

### Monitoring Pool Status

The `SessionPool` class exposes real-time health metrics that can be used for monitoring and alerting:

```csharp
var sessionPool = new SessionPool.Builder()
    .SetHost("127.0.0.1")
    .SetPort(6667)
    .SetPoolSize(8)
    .Build();

await sessionPool.Open();

// Check pool health
Console.WriteLine($"Available Clients: {sessionPool.AvailableClients}");
Console.WriteLine($"Total Pool Size: {sessionPool.TotalPoolSize}");
Console.WriteLine($"Unrealized Capacity: {sessionPool.UnrealizedCapacity}");
Console.WriteLine($"Failed Reconnections: {sessionPool.FailedReconnections}");
```

### Health Metrics

| Metric               | Property              | Description                                      | Recommended Threshold       |
| -------------------- | --------------------- | ------------------------------------------------ | --------------------------- |
| Available Clients    | `AvailableClients`    | Number of idle clients ready for use             | Alert if < 25% of pool size |
| Total Pool Size      | `TotalPoolSize`       | Configured maximum pool size                     | N/A (constant)              |
| Unrealized Capacity  | `UnrealizedCapacity`  | Configured capacity currently holding no connection, refilled on demand | Not an alert signal on its own - see below |
| Failed Reconnections | `FailedReconnections` | Cumulative count of failed reconnection attempts | Alert if > 0 and increasing |

### Capacity is demand-driven

When an operation fails and reconnection also fails, the dead connection is discarded but its **capacity is
retained** rather than lost. `UnrealizedCapacity` counts the capacity left without a connection, and an
acquisition that finds no idle client materializes one connection before falling back to waiting.
Consequences:

- The pool no longer shrinks by one on every failure, so it cannot reach the state where every caller blocks
  on a queue nobody will feed.
- Once the server is reachable again, the pool repopulates itself as load demands it - no `Close()` +
  `Open()` cycle is required.
- **Capacity is refilled on demand, not eagerly.** A connection is only created when an acquisition finds the
  idle queue empty. Under sequential or light workloads one connection is enough to serve every request, so
  `UnrealizedCapacity` legitimately stays above zero long after the server has fully recovered. It measures
  how much of the configured pool has not been materialized, not server availability.
- Therefore **do not alert on `UnrealizedCapacity` alone.** Use `FailedReconnections` to reason about server
  reachability: it only increases when a reconnection actually fails.

## Failure Scenarios and Recovery Strategies

### Scenario 1: Pool Exhaustion (High Load)

**Symptoms:**

- `SessionPoolDepletedException` with reason "Connection pool is empty and wait time out"
- `AvailableClients` = 0
- `FailedReconnections` = 0 or low

**Root Cause:** Application workload exceeds pool capacity

**Recovery Strategies:**

1. **Increase Pool Size:**

```csharp
var sessionPool = new SessionPool.Builder()
    .SetHost("127.0.0.1")
    .SetPort(6667)
    .SetPoolSize(16)  // Increased from 8
    .Build();
```

2. **Implement Connection Retry with Backoff:**

```csharp
int maxRetries = 3;
int retryDelayMs = 1000;

for (int i = 0; i < maxRetries; i++)
{
    try
    {
        await sessionPool.InsertRecordAsync(deviceId, record);
        break;  // Success
    }
    catch (SessionPoolDepletedException ex) when (i < maxRetries - 1)
    {
        await Task.Delay(retryDelayMs * (i + 1));  // Exponential backoff
    }
}
```

3. **Optimize Operation Duration:**
    - Reduce the time each client is held
    - Batch multiple operations together
    - Use async operations efficiently

### Scenario 2: Network Connectivity Issues

**Symptoms:**

- `SessionPoolDepletedException` with reason "Reconnection failed"
- `AvailableClients` drops toward 0 while `UnrealizedCapacity` rises
- `FailedReconnections` > 0 and increasing (this, not `UnrealizedCapacity`, is the outage signal)

**Root Cause:** IoTDB server unreachable or network issues

**Recovery Strategies:**

0. **Do nothing but retry.** Capacity is retained and refilled on demand, so once the server comes back a
   plain retry succeeds. Reinitialising is only needed if you want to change configuration or drop
   accumulated state:

```csharp
catch (SessionPoolDepletedException ex)
{
    // Capacity is retained and refilled on demand - just back off and try again
    await Task.Delay(2000);
}
```

1. **Reinitialize SessionPool:**

```csharp
catch (SessionPoolDepletedException ex) when (ex.FailedReconnections > 5)
{
    Console.WriteLine($"Critical: {ex.FailedReconnections} failed reconnections");

    // Close existing pool
    await sessionPool.Close();

    // Wait for network recovery
    await Task.Delay(5000);

    // Create new pool
    sessionPool = new SessionPool.Builder()
        .SetHost("127.0.0.1")
        .SetPort(6667)
        .SetPoolSize(8)
        .Build();

    await sessionPool.Open();
}
```

2. **Implement Circuit Breaker Pattern:**

```csharp
public class SessionPoolCircuitBreaker
{
    private SessionPool _pool;
    private int _failureCount = 0;
    private const int FailureThreshold = 5;
    private bool _circuitOpen = false;
    private DateTime _lastFailureTime;

    public async Task<T> ExecuteAsync<T>(Func<SessionPool, Task<T>> operation)
    {
        if (_circuitOpen && DateTime.Now - _lastFailureTime < TimeSpan.FromMinutes(1))
        {
            throw new Exception("Circuit breaker is open");
        }

        try
        {
            var result = await operation(_pool);
            _failureCount = 0;  // Reset on success
            _circuitOpen = false;
            return result;
        }
        catch (SessionPoolDepletedException ex)
        {
            _failureCount++;
            _lastFailureTime = DateTime.Now;

            if (_failureCount >= FailureThreshold)
            {
                _circuitOpen = true;
                Console.WriteLine("Circuit breaker opened - too many failures");
            }
            throw;
        }
    }
}
```

### Scenario 3: Server Overload

**Symptoms:**

- Intermittent `SessionPoolDepletedException`
- Both connection timeouts and reconnection failures

**Root Cause:** IoTDB server is overloaded

**Recovery Strategies:**

1. **Implement Rate Limiting:**

```csharp
using System.Threading;

private SemaphoreSlim _rateLimiter = new SemaphoreSlim(10, 10);  // Max 10 concurrent operations

public async Task RateLimitedInsert(string deviceId, RowRecord record)
{
    await _rateLimiter.WaitAsync();
    try
    {
        await sessionPool.InsertRecordAsync(deviceId, record);
    }
    finally
    {
        _rateLimiter.Release();
    }
}
```

2. **Add Timeout Configuration:**

```csharp
var sessionPool = new SessionPool.Builder()
    .SetHost("127.0.0.1")
    .SetPort(6667)
    .SetConnectionTimeoutInMs(5000)  // Increased socket timeout for a slow server
    .Build();
```

## Monitoring and Alerting Recommendations

### Health Check Implementation

```csharp
public class SessionPoolHealthCheck
{
    private readonly SessionPool _pool;

    public SessionPoolHealthCheck(SessionPool pool)
    {
        _pool = pool;
    }

    public HealthStatus CheckHealth()
    {
        var availableRatio = (double)_pool.AvailableClients / _pool.TotalPoolSize;

        if (_pool.FailedReconnections > 10)
        {
            return new HealthStatus
            {
                Status = "Critical",
                Message = $"High reconnection failures: {_pool.FailedReconnections}",
                Recommendation = "Check IoTDB server availability"
            };
        }

        if (availableRatio < 0.25)
        {
            return new HealthStatus
            {
                Status = "Warning",
                Message = $"Low available clients: {_pool.AvailableClients}/{_pool.TotalPoolSize}",
                Recommendation = "Consider increasing pool size"
            };
        }

        return new HealthStatus
        {
            Status = "Healthy",
            Message = $"Pool healthy: {_pool.AvailableClients}/{_pool.TotalPoolSize} available"
        };
    }
}

public class HealthStatus
{
    public string Status { get; set; }
    public string Message { get; set; }
    public string Recommendation { get; set; }
}
```

### Metrics Collection for Monitoring Systems

```csharp
// Example: Export metrics to Prometheus, StatsD, or similar
public class SessionPoolMetricsCollector
{
    private readonly SessionPool _pool;

    public void CollectMetrics()
    {
        // Gauge: Current available clients
        MetricsCollector.Set("iotdb_pool_available_clients", _pool.AvailableClients);

        // Gauge: Total pool size
        MetricsCollector.Set("iotdb_pool_total_size", _pool.TotalPoolSize);

        // Counter: Failed reconnections
        MetricsCollector.Set("iotdb_pool_failed_reconnections", _pool.FailedReconnections);

        // Calculated: Pool utilization percentage
        var utilization = (1.0 - (double)_pool.AvailableClients / _pool.TotalPoolSize) * 100;
        MetricsCollector.Set("iotdb_pool_utilization_percent", utilization);
    }
}
```

### Recommended Alert Rules

1. **Critical Alerts:**
    - `FailedReconnections > 10`: Server connectivity issues
    - `AvailableClients == 0` for > 30 seconds: Complete pool exhaustion

2. **Warning Alerts:**
    - `AvailableClients < TotalPoolSize * 0.25`: Pool under pressure
    - `FailedReconnections > 0` and increasing: Network instability

3. **Info Alerts:**
    - Pool utilization > 75% for extended periods: Consider scaling

## Best Practices

1. **Pool Sizing:**
    - Start with poolSize = 2 × expected concurrent operations
    - Monitor and adjust based on actual usage patterns
    - Larger pools use more server resources but provide better throughput

2. **Error Handling:**
    - Always catch `SessionPoolDepletedException` specifically
    - Log exception properties for debugging
    - Implement appropriate retry logic based on depletion reason

3. **Monitoring:**
    - Continuously monitor `AvailableClients` metric
    - Track `FailedReconnections` as a leading indicator of problems
    - Set up alerts before pool is completely depleted

4. **Resource Management:**
    - Always call `sessionPool.Close()` when done
    - Use `using` statements or try-finally blocks for proper cleanup
    - Don't create multiple SessionPool instances unnecessarily

## Example: Complete Production-Ready Implementation

```csharp
using Apache.IoTDB;
using System;
using System.Threading.Tasks;

public class ProductionSessionPoolManager
{
    private SessionPool _pool;
    private readonly object _lock = new object();

    public async Task Initialize()
    {
        _pool = new SessionPool.Builder()
            .SetHost("127.0.0.1")
            .SetPort(6667)
            .SetPoolSize(8)
            .SetConnectionTimeoutInMs(5000)
            .Build();

        await _pool.Open();

        // Start health monitoring
        _ = Task.Run(MonitorHealth);
    }

    public async Task<T> ExecuteWithRetry<T>(Func<SessionPool, Task<T>> operation)
    {
        const int maxRetries = 3;
        const int baseDelayMs = 1000;

        for (int attempt = 0; attempt < maxRetries; attempt++)
        {
            try
            {
                return await operation(_pool);
            }
            catch (SessionPoolDepletedException ex)
            {
                Console.WriteLine($"Attempt {attempt + 1} failed: {ex.Message}");
                Console.WriteLine($"Pool state - Available: {ex.AvailableClients}/{ex.TotalPoolSize}, Failed reconnections: {ex.FailedReconnections}");

                if (attempt == maxRetries - 1)
                {
                    // Last attempt failed
                    if (ex.FailedReconnections > 5)
                    {
                        // Reinitialize pool
                        await ReinitializePool();
                    }
                    throw;
                }

                // Exponential backoff
                await Task.Delay(baseDelayMs * (int)Math.Pow(2, attempt));
            }
        }

        throw new InvalidOperationException("Should not reach here");
    }

    private async Task ReinitializePool()
    {
        lock (_lock)
        {
            try
            {
                _pool?.Close().Wait();
            }
            catch { }
        }

        await Task.Delay(5000);  // Wait for server recovery
        await Initialize();
    }

    private async Task MonitorHealth()
    {
        while (true)
        {
            await Task.Delay(10000);  // Check every 10 seconds

            try
            {
                var availableRatio = (double)_pool.AvailableClients / _pool.TotalPoolSize;

                if (_pool.FailedReconnections > 10)
                {
                    Console.WriteLine($"CRITICAL: {_pool.FailedReconnections} failed reconnections");
                }
                else if (availableRatio < 0.25)
                {
                    Console.WriteLine($"WARNING: Low available clients - {_pool.AvailableClients}/{_pool.TotalPoolSize}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Health check failed: {ex.Message}");
            }
        }
    }

    public async Task Cleanup()
    {
        await _pool?.Close();
    }
}
```

## Summary

The SessionPool exception handling and health monitoring features provide comprehensive tools for building robust IoTDB applications:

- Use `SessionPoolDepletedException` to understand and react to pool issues
- Treat `IsOpen()` as a lifecycle flag, never as a connectivity check
- Tune `SetPoolWaitTimeoutInMs` separately from `SetConnectionTimeoutInMs`
- Monitor `AvailableClients`, `TotalPoolSize`, and `FailedReconnections`; read `UnrealizedCapacity` as
  capacity not yet materialized rather than as an outage signal
- Rely on demand-driven capacity refill for recovery; reinitialise only when you need to change configuration
- Implement appropriate recovery strategies based on failure scenarios
- Set up proactive monitoring and alerting to prevent issues
- Follow best practices for pool sizing and resource management
