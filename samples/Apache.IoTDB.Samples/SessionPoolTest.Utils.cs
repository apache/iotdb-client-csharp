using System;
using System.Collections.Generic;
using Apache.IoTDB.DataStructure;

namespace Apache.IoTDB.Samples
{
    public partial class SessionPoolTest
    {
        public static void PrintDataSetByObject(SessionDataSet sessionDataSet)
        {
            IReadOnlyList<string> columns = sessionDataSet.GetColumnNames();

            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            while (sessionDataSet.HasNext())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string columnName = columns[i];
                    Console.Write(sessionDataSet.GetObject(columnName));
                    Console.Write("\t\t");
                }
                Console.WriteLine();
            }
        }

        public static void PrintDataSetByString(SessionDataSet sessionDataSet)
        {
            IReadOnlyList<string> columns = sessionDataSet.GetColumnNames();

            foreach (string columnName in columns)
            {
                Console.Write($"{columnName}\t");
            }
            Console.WriteLine();

            while (sessionDataSet.HasNext())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    string columnName = columns[i];
                    Console.Write(sessionDataSet.GetString(columnName));
                    Console.Write("\t\t");
                }
                Console.WriteLine();
            }
        }
        
    }
}