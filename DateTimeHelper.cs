using System;
using System.Windows.Controls;


namespace CLib
{
    /// <summary>
    /// Date and Times Helper
    /// </summary>
    public class DateTimeHelper
    {

        /// <summary>
        /// Return le nombre de Mois entre deux dates
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int MonthDifference(DateTime fromDate, DateTime toDate)
        {
            var earlyDate = (fromDate > toDate) ? toDate.Date : fromDate.Date;
            var lateDate = (fromDate > toDate) ? fromDate.Date : toDate.Date;

            // Start with 1 month's difference and keep incrementing
            // until we overshoot the late date
            var monthsDiff = 1;
            while (earlyDate.AddMonths(monthsDiff) <= lateDate)
                monthsDiff++;

            return monthsDiff - 1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string ProperTimeSpan(TimeSpan ts)
        {
            var time = String.Format("{0:00}", ts.Minutes);   //:{1:00}
            var hours = (int)ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string HourTo12(int hour)
        {
            if (hour == 0)
            {
                return 12.ToString();
            }
            if (hour > 12)
            {
                return AddZeroes(hour - 12);
            }
            return AddZeroes(hour);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string AddZeroes(int number, int digits = 2)
        {
            return number.ToString().PadLeft(digits, '0');
        }
    }
}
