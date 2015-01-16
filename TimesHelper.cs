using System;


namespace CLib
{
    /// <summary>
    /// Date and Times Helper
    /// </summary>
    public class TimesHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static string ProperTimeSpan(TimeSpan ts)
        {
            var time = string.Format("{0:00}", ts.Minutes);   //:{1:00}
            var hours = (int)ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }


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

        public static string AddZeroes(int number, int digits = 2)
        {
            return number.ToString().PadLeft(digits, '0');
        }









    }
}
