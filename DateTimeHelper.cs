using System;
using System.Collections.Generic;

namespace CLib
{
    /// <summary>
    /// Date and Times Helper
    /// </summary>
    public static class DateTimeHelper
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
        public static string ProperTimeSpan(this TimeSpan ts)
        {
            var time = string.Format("{0:00}", ts.Minutes);   //:{1:00}
            var hours = (int)ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string TotalHoursMins (this TimeSpan timeSpan) {
            var hours = timeSpan.Hours;
            var minutes = timeSpan.Minutes;

            return hours>0 ? string.Format("{0} hrs {1} mins", hours, minutes) : string.Format("{0} minutes", minutes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableAgeString (this TimeSpan span) {
            return string.Format("{0:0}", span.Days/365.25);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString (this TimeSpan span) {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days>0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days==1 ? String.Empty : "s") : string.Empty,
                span.Duration().Hours>0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours==1 ? String.Empty : "s") : string.Empty,
                span.Duration().Minutes>0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes==1 ? String.Empty : "s") : string.Empty,
                span.Duration().Seconds>0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds==1 ? String.Empty : "s") : string.Empty);

            if(formatted.EndsWith(", "))
                formatted=formatted.Substring(0, formatted.Length-2);

            if(string.IsNullOrEmpty(formatted))
                formatted="0 seconds";

            return formatted;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachDay (DateTime from, DateTime thru) {
            for(var day = from.Date; day.Date<=thru.Date; day=day.AddDays(1))
                yield return day;
            //foreach(DateTime day in EachDay(StartDate, EndDate))
                // print it or whatever
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachMonth (DateTime from, DateTime thru) {
            for(var month = from.Date; month.Date<=thru.Date||month.Month==thru.Month; month=month.AddMonths(1))
                yield return month;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> WeekDays (DateTime myDate) {            
            var firstDateOfWeek = myDate.DayOfWeek==DayOfWeek.Sunday ? myDate.AddDays(-6) : myDate.AddDays(-((int)myDate.DayOfWeek-1));

            for(var day = firstDateOfWeek.Date; day.Date <=firstDateOfWeek.AddDays(6).Date; day=day.AddDays(1))
                yield return day;
        }



    }
}
