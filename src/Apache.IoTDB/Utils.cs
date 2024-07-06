using System;
using System.Collections.Generic;
using System.Linq;

namespace Apache.IoTDB
{
    public class Utils
    {
        public bool IsSorted(IList<long> collection)
        {
            for (var i = 1; i < collection.Count; i++)
            {
                if (collection[i] < collection[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        public int VerifySuccess(TSStatus status)
        {
            if (status.Code == (int)TSStatusCode.MULTIPLE_ERROR)
            {
                if (status.SubStatus.Any(subStatus => VerifySuccess(subStatus) != 0))
                {
                    return -1;
                }
                return 0;
            }
            if (status.Code == (int)TSStatusCode.REDIRECTION_RECOMMEND)
            {
                return 0;
            }
            if (status.Code == (int)TSStatusCode.SUCCESS_STATUS)
            {
                return 0;
            }
            return -1;
        }
    }
}