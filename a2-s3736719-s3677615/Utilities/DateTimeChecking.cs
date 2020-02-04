using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace a2_s3736719_s3677615.Utilities
{
    // datatime check for periodically bill pay
    public static class DateTimeChecking
    {
        // once time
        public static bool SameDifferentMin(DateTime scheduelDate, DateTime rightNow)
        {
            return scheduelDate.Year == rightNow.Year &&
                   scheduelDate.Month == rightNow.Month &&
                   scheduelDate.Day == rightNow.Day &&
                   scheduelDate.Hour == rightNow.Hour &&
                   scheduelDate.Minute == rightNow.Minute;
        }
        // annually judge datas are same or not
        public static bool SameDifferentYear(DateTime scheduelDate, DateTime rightNow)
        {
            return scheduelDate.Month == rightNow.Month &&
                   scheduelDate.Day == rightNow.Day &&
                   scheduelDate.Hour == rightNow.Hour &&
                   scheduelDate.Minute == rightNow.Minute;
        }

        // monthly judge dates are same or not
        public static bool SameDifferentMonth(DateTime scheduelDate, DateTime rightNow)
        { 
            
            return scheduelDate.Day == rightNow.Day &&
                   scheduelDate.Hour == rightNow.Hour &&
                   scheduelDate.Minute == rightNow.Minute;
        }

        // Quarterly judge dates are same or not
        public static bool SameDifferentQuarter(DateTime scheduelDate, DateTime rightNow)
        {
            var monthGap = (scheduelDate.Year - rightNow.Year)*12 + scheduelDate.Month -rightNow.Month;
            if (monthGap % 3 != 0) return false;

            return scheduelDate.Day == rightNow.Day &&
                   scheduelDate.Hour == rightNow.Hour &&
                   scheduelDate.Minute == rightNow.Minute;
        }
    }
}
