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
        













    }
}
