using System;
using System.Collections.Generic;
using System.Linq;
using eLib.Enums;

namespace eLib.Utils
{
    /// <summary>
    /// Date and Times Helper
    /// </summary>
    public static class DateTimeHelper
    {
        #region Extention Mine



        public static List<KeyValuePair<string, KeyValuePair<DateTime, DateTime>>> Intervals(DateTime from, DateTime thru, PeriodInterval interval)
        {
            var lists = new List<KeyValuePair<string, KeyValuePair<DateTime, DateTime>>>();

            switch (interval)
            {
                case PeriodInterval.Once:
                    return new List<KeyValuePair<string, KeyValuePair<DateTime, DateTime>>>
                                                  { new KeyValuePair<string, KeyValuePair<DateTime, DateTime>>("All", new KeyValuePair<DateTime, DateTime>(from, thru))};
                case PeriodInterval.Monthly:
                    from = new DateTime(from.Year, from.Month, 1);
                    break;
                case PeriodInterval.Quarterly:
                    if (from.Month < 4)
                        from = new DateTime(from.Year, 1, 1);
                    else if (from.Month < 7)
                        from = new DateTime(from.Year, 4, 1);
                    else if (from.Month < 10)
                        from = new DateTime(from.Year, 7, 1);
                    else
                        from = new DateTime(from.Year, 10, 1);
                    break;
                case PeriodInterval.HalfYearly:
                    @from = @from.Month < 7 ? new DateTime(@from.Year, 1, 1) : new DateTime(@from.Year, 7, 1);
                    break;
                case PeriodInterval.Yearly:
                    from = new DateTime(from.Year, 1, 1);
                    break;
                case PeriodInterval.WklySalary:
                    lists.AddRange(from date in EachWeek(@from, thru)
                                   let periode = Interval(date, interval)
                                   select new KeyValuePair<string, KeyValuePair<DateTime, DateTime>>(periode.Key, new KeyValuePair<DateTime, DateTime>(date, periode.Value)));
                    return lists;
                case PeriodInterval.BiWklySalary:
                    lists.AddRange(from date in EachWeek(@from, thru, 2)
                                   let periode = Interval(date, interval)
                                   select new KeyValuePair<string, KeyValuePair<DateTime, DateTime>>(periode.Key, new KeyValuePair<DateTime, DateTime>(date, periode.Value)));
                    return lists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }

            lists.AddRange(from date in EachMonth(@from, thru, (int)interval)
                           let periode = Interval(date, interval)
                           select new KeyValuePair<string, KeyValuePair<DateTime, DateTime>>(periode.Key, new KeyValuePair<DateTime, DateTime>(date, periode.Value)));
            return lists;
        }

        public static KeyValuePair<string, DateTime> Interval(DateTime from, PeriodInterval interval)
        {
            var next = from.Date.AddMonths((int)interval).AddDays(-1);

            switch (interval)
            {
                case PeriodInterval.Once:
                    return new KeyValuePair<string, DateTime>(next.ToShortDateString(), next);
                case PeriodInterval.Monthly:
                    return new KeyValuePair<string, DateTime>(next.ToString("MMM-yy"), next);
                case PeriodInterval.Quarterly:
                    if (next.Month < 4)
                        return new KeyValuePair<string, DateTime>(string.Format("Q1 {0}", next.ToString("yyyy")), next);
                    if (next.Month < 7)
                        return new KeyValuePair<string, DateTime>(string.Format("Q2 {0}", next.ToString("yyyy")), next);
                    return next.Month < 10 ? new KeyValuePair<string, DateTime>(string.Format("Q3 {0}", next.ToString("yyyy")), next) : new KeyValuePair<string, DateTime>(string.Format("Q4 {0}", next.ToString("yyyy")), next);
                case PeriodInterval.HalfYearly:
                    return next.Month < 7 ? new KeyValuePair<string, DateTime>(string.Format("S1 {0}", next.ToString("yyyy")), next) : new KeyValuePair<string, DateTime>(string.Format("S2 {0}", next.ToString("yyyy")), next);
                case PeriodInterval.Yearly:
                    return new KeyValuePair<string, DateTime>(next.ToString("yyyy"), next);
                case PeriodInterval.WklySalary:
                    next = from.AddDays(7);
                    return new KeyValuePair<string, DateTime>(next.ToShortDateString(), next);
                case PeriodInterval.BiWklySalary:
                    next = from.AddDays(14);
                    return new KeyValuePair<string, DateTime>(next.ToShortDateString(), next);
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }
        }


        /// <summary>
        /// Return le nombre de Mois entre deux dates
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public static int MonthDifference(DateTime fromDate, DateTime toDate)
        {
            var earlyDate = fromDate > toDate ? toDate.Date : fromDate.Date;
            var lateDate = fromDate > toDate ? fromDate.Date : toDate.Date;

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
            var time = $"{ts.Minutes:00}"; //:{1:00}
            var hours = (int) ts.TotalHours;
            if (hours > 0) time = hours + ":" + time;
            return time;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string AsTimeSpan(this TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
                return "0";

            var hours = (int) timeSpan.TotalHours;
            var minutes = timeSpan.Minutes;

            return hours > 0 ? $"{hours} hrs {minutes} mins" : $"{minutes} minutes";
        }

        public static string AsTime(this TimeSpan timeSpan) => $"{timeSpan.Hours:00}H:{timeSpan.Minutes:00}";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableAgeString(this TimeSpan span)
        {
            return $"{span.Days/365.25:0}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public static string ToReadableString(this TimeSpan span)
        {
            var formatted =
                $"{(span.Duration().Days > 0 ? $"{span.Days:0} day{(span.Days == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Hours > 0 ? $"{span.Hours:0} hour{(span.Hours == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Minutes > 0 ? $"{span.Minutes:0} minute{(span.Minutes == 1 ? string.Empty : "s")}, " : string.Empty)}{(span.Duration().Seconds > 0 ? $"{span.Seconds:0} second{(span.Seconds == 1 ? string.Empty : "s")}" : string.Empty)}";

            if (formatted.EndsWith(", "))
                formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted))
                formatted = "0 seconds";

            return formatted;
        }

        /// <summary>
        /// Format la date a une Date friendly
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string Friendly(this DateTime? dateTime)
        {
            var date = dateTime.GetValueOrDefault();

            if (date.Date == DateTime.Today)
                return date.ToString("hh\\:mm");
            if (date.Date == DateTime.Today.AddDays(-1))
                return "Hier a " + date.ToString("hh\\:mm");
            return date.ToShortDateString() + "-" + date.ToString("hh\\:mm");
        }

        /// <summary>
        /// Format la date a une Date friendly
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FriendlyDateTime(this DateTime? dateTime)
        {
            var date = dateTime.GetValueOrDefault();

            if (date.Date == DateTime.Today)
                return $"aujourd'hui à {date.ToString("hh\\:mm")}";
            return date.Date == DateTime.Today.AddDays(-1)
                ? "hier à " + date.ToString("hh\\:mm")
                : $"le {date.ToString("dd MMM yyy")}";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static string HourTo12(int hour)
            => hour == 0 ? 12.ToString() : (hour > 12 ? AddZeroes(hour - 12) : AddZeroes(hour));


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
        ///  foreach(DateTime day in EachDay(StartDate, EndDate))
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static IEnumerable<DateTime> EachWeek(DateTime from, DateTime thru, int interval = 1)
        {
            if (interval == 0)
                throw new NotSupportedException("Interval can not be zero");

            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(interval * 7))
                yield return day;
        }

        /// <summary>
        /// Chaque Mois
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru, int interval = 1)
        {
            if (interval == 0)
                throw new NotSupportedException("Interval can not be zero");

            for (var month = from.Date; month.Date <= thru.Date; month = month.AddMonths(interval))
                yield return month;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="thru"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> EachYear(DateTime from, DateTime thru, int interval = 1)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddYears(interval))
                yield return day;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="myDate"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> WeekDates(DateTime myDate)
        {
            var firstDateOfWeek = myDate.DayOfWeek == DayOfWeek.Sunday ? myDate.AddDays(-6) : myDate.AddDays(-((int) myDate.DayOfWeek - 1));

            for (var day = firstDateOfWeek.Date; day.Date <= firstDateOfWeek.AddDays(6).Date; day = day.AddDays(1))
                yield return day;
        }


        /// <summary>
        /// Increases supplied <see cref="DateTime"/> for 7 days ie returns the Next Week.
        /// </summary>
        public static DateTime WeekAfter(this DateTime start)
        {
            return start.AddDays(7);
        }

        /// <summary>
        /// Decreases supplied <see cref="DateTime"/> for 7 days ie returns the Previous Week.
        /// </summary>
        public static DateTime WeekEarlier(this DateTime start)
        {
            return start.AddDays(-7);
        }

        /// <summary>
        /// Returns first next occurrence of specified <see cref="DayOfWeek"/>.
        /// </summary>
        public static DateTime Next(this DateTime start, DayOfWeek day)
        {
            do
            {
                start = start.NextDay();
            } while (start.DayOfWeek != day);

            return start;
        }

        #endregion

        #region Extention 1

        /// <summary>
        /// Retrives the first day of the month of the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">A date from the month we want to get the first day.</param>
        /// <returns>A DateTime representing the first day of the month.</returns>
        public static DateTime FirstDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Retrives the last day of the month of the <paramref name="date"/>.
        /// </summary>
        /// <param name="date">A date from the month we want to get the last day.</param>
        /// <returns>A DateTime representing the last day of the month.</returns>
        public static DateTime LastDayOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Retrives the last day of the week that occourred since <paramref name="date"/>.
        /// </summary>
        /// <remarks>If <paramref name="dayOfweek"/>.DayOfWeek is already <paramref name="dayOfweek"/>, it will return the last one (seven days before)</remarks>
        /// <param name="date">A date.</param>
        /// <param name="dayOfweek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the last day of the week that occourred.</returns>
        public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek dayOfweek)
        {
            var delta = -7;
            DateTime targetDate;
            do
            {
                targetDate = date.AddDays(delta);
                delta++;
            } while (targetDate.DayOfWeek != dayOfweek);
            return targetDate;
        }

        /// <summary>
        /// Retrives the next day of the week that will occour after <paramref name="date"/>.
        /// </summary>
        /// <remarks>If <paramref name="dayOfweek"/>.DayOfWeek is already <paramref name="dayOfweek"/>, it will return the next one (seven days after)</remarks>
        /// <param name="date">A date.</param>
        /// <param name="dayOfweek">The kind of DayOfWeek we want to get.</param>
        /// <returns>A DateTime representing the next day of the week that will occour after.</returns>
        public static DateTime NextDayOfWeek(this DateTime date, DayOfWeek dayOfweek)
        {
            var delta = 7;
            DateTime targetDate;
            do
            {
                targetDate = date.AddDays(delta);
                delta--;
            } while (targetDate.DayOfWeek != dayOfweek);
            return targetDate;
        }

        public static DateTime LastDayOfWeekOfTheMonth(this DateTime date, DayOfWeek dayOfweek)
        {
            var lastDayOfTheMonth = date.LastDayOfTheMonth();
            if (lastDayOfTheMonth.DayOfWeek == dayOfweek)
            {
                return lastDayOfTheMonth;
            }
            return lastDayOfTheMonth.LastDayOfWeek(dayOfweek);
        }

        public static DateTime FirstDayOfWeekOfTheMonth(this DateTime date, DayOfWeek dayOfweek)
        {
            var firstDayOfTheMonth = date.FirstDayOfTheMonth();
            if (firstDayOfTheMonth.DayOfWeek == dayOfweek)
            {
                return firstDayOfTheMonth;
            }
            return firstDayOfTheMonth.NextDayOfWeek(dayOfweek);
        }

        public static DateTime SetTime(this DateTime date, int hour)
        {
            return date.SetTime(hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute)
        {
            return date.SetTime(hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second)
        {
            return date.SetTime(hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }

        #endregion

        #region Date Extention

        public static int ToEpoch(this DateTime fromDate)
        {
            var utc = (fromDate.ToUniversalTime().Ticks - EpochTicks)/TimeSpan.TicksPerSecond;
            return Convert.ToInt32(utc);
        }

        public static int ToEpochOffset(this DateTime date, int timestamp)
        {
            return timestamp - date.ToEpoch();
        }

        public static int ToEpoch(this DateTime date, int offset)
        {
            return offset + date.ToEpoch();
        }

        private const long EpochTicks = 621355968000000000;

        public static DateTime ToDateTime(this int secondsSinceEpoch)
        {
            return new DateTime(EpochTicks + secondsSinceEpoch*TimeSpan.TicksPerSecond);
        }

        public static DateTime ToDateTime(this double milliSecondsSinceEpoch)
        {
            return new DateTime(EpochTicks + (long) milliSecondsSinceEpoch*TimeSpan.TicksPerMillisecond);
        }

        public static DateTime ChangeMillisecond(this DateTime date, int millisecond)
        {
            if (millisecond < 0 || millisecond > 59)
                throw new ArgumentException("Value must be between 0 and 999.", nameof(millisecond));

            return date.AddMilliseconds(millisecond - date.Millisecond);
        }

        public static DateTime ChangeSecond(this DateTime date, int second)
        {
            if (second < 0 || second > 59)
                throw new ArgumentException("Value must be between 0 and 59.", nameof(second));

            return date.AddSeconds(second - date.Second);
        }

        public static DateTime ChangeMinute(this DateTime date, int minute)
        {
            if (minute < 0 || minute > 59)
                throw new ArgumentException("Value must be between 0 and 59.", nameof(minute));

            return date.AddMinutes(minute - date.Minute);
        }

        public static DateTime ChangeHour(this DateTime date, int hour)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentException("Value must be between 0 and 23.", nameof(hour));

            return date.AddHours(hour - date.Hour);
        }

        public static DateTime ChangeDay(this DateTime date, int day)
        {
            if (day < 1 || day > 31)
                throw new ArgumentException("Value must be between 1 and 31.", nameof(day));

            if (day > DateTime.DaysInMonth(date.Year, date.Month))
                throw new ArgumentException("Value must be a valid day.", nameof(day));

            return date.AddDays(day - date.Day);
        }

        public static DateTime ChangeMonth(this DateTime date, int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentException("Value must be between 1 and 12.", nameof(month));

            return date.AddMonths(month - date.Month);
        }

        public static DateTime ChangeYear(this DateTime date, int year)
        {
            return date.AddYears(year - date.Year);
        }

        public static DateTime Change(this DateTime date, int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null)
        {
            var result = date;

            if (year.HasValue)
                result = result.ChangeYear(year.Value);
            if (month.HasValue)
                result = result.ChangeMonth(month.Value);
            if (day.HasValue)
                result = result.ChangeDay(day.Value);
            if (hour.HasValue)
                result = result.ChangeHour(hour.Value);
            if (minute.HasValue)
                result = result.ChangeMinute(minute.Value);
            if (second.HasValue)
                result = result.ChangeSecond(second.Value);

            return result;
        }

        public static DateTime StartOfSecond(this DateTime date)
        {
            return date.Floor(TimeSpan.FromSeconds(1));
        }

        public static DateTime EndOfSecond(this DateTime date)
        {
            return date.StartOfSecond().AddSeconds(1).SubtractMilliseconds(1);
        }

        public static DateTime StartOfMinute(this DateTime date)
        {
            return date.Floor(TimeSpan.FromMinutes(1));
        }

        public static DateTime EndOfMinute(this DateTime date)
        {
            return date.StartOfMinute().AddMinutes(1).SubtractMilliseconds(1);
        }

        public static DateTime StartOfHour(this DateTime date)
        {
            return date.Floor(TimeSpan.FromHours(1));
        }

        public static DateTime EndOfHour(this DateTime date)
        {
            return date.StartOfHour().AddHours(1).SubtractMilliseconds(1);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).SubtractMilliseconds(1);
        }

        public static DateTime StartOfDay(this DateTime date)
        {
            return date.Date;
        }

        public static DateTime StartOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            var diff = date.DayOfWeek - startOfWeek;
            if (diff < 0)
                diff += 7;

            return date.Date.AddDays(-1*diff);
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            return date.StartOfWeek(startOfWeek).AddWeeks(1).SubtractMilliseconds(1);
        }

        public static DateTime StartOfMonth(this DateTime date)
        {
            return date.Date.AddDays(1 - date.Date.Day);
        }

        public static DateTime EndOfMonth(this DateTime date)
        {
            return date.StartOfMonth().AddMonths(1).SubtractMilliseconds(1);
        }

        public static DateTime StartOfYear(this DateTime date)
        {
            return date.Date.AddDays(1 - date.Date.Day).AddMonths(1 - date.Date.Month);
        }

        public static DateTime EndOfYear(this DateTime date)
        {
            return date.StartOfYear().AddYears(1).SubtractMilliseconds(1);
        }

        public static DateTime Floor(this DateTime date, TimeSpan interval)
        {
            return date.AddTicks(-(date.Ticks%interval.Ticks));
        }

        public static DateTime Ceiling(this DateTime date, TimeSpan interval)
        {
            return date.AddTicks(interval.Ticks - date.Ticks%interval.Ticks);
        }

        public static DateTime Round(this DateTime date, TimeSpan roundingInterval)
        {
            var halfIntervalTicks = (roundingInterval.Ticks + 1) >> 1;
            return date.AddTicks(halfIntervalTicks - (date.Ticks + halfIntervalTicks)%roundingInterval.Ticks);
        }

        public static DateTime NextSecond(this DateTime date)
        {
            return date.AddSeconds(1);
        }

        public static DateTime LastSecond(this DateTime date)
        {
            return date.SubtractSeconds(1);
        }

        public static DateTime NextMinute(this DateTime date)
        {
            return date.AddMinutes(1);
        }

        public static DateTime LastMinute(this DateTime date)
        {
            return date.SubtractMinutes(1);
        }

        public static DateTime NextHour(this DateTime date)
        {
            return date.AddHours(1);
        }

        public static DateTime LastHour(this DateTime date)
        {
            return date.SubtractHours(1);
        }

        public static DateTime NextDay(this DateTime date)
        {
            return date.AddDays(1);
        }

        public static DateTime LastDay(this DateTime date)
        {
            return date.SubtractDays(1);
        }

        public static DateTime NextWeek(this DateTime date)
        {
            return date.AddWeeks(1);
        }

        public static DateTime LastWeek(this DateTime date)
        {
            return date.SubtractWeeks(1);
        }

        public static DateTime NextMonth(this DateTime date)
        {
            return date.AddMonths(1);
        }

        public static DateTime LastMonth(this DateTime date)
        {
            return date.SubtractMonths(1);
        }

        public static DateTime NextYear(this DateTime date)
        {
            return date.AddYears(1);
        }

        public static DateTime LastYear(this DateTime date)
        {
            return date.SubtractYears(1);
        }

        public static DateTime SubtractTicks(this DateTime date, long value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddTicks(value*-1);
        }

        public static DateTime SubtractMilliseconds(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddMilliseconds(value*-1);
        }

        public static DateTime SubtractSeconds(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddSeconds(value*-1);
        }

        public static DateTime SubtractMinutes(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddMinutes(value*-1);
        }

        public static DateTime SubtractHours(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddHours(value*-1);
        }

        public static DateTime SubtractDays(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddDays(value*-1);
        }

        public static DateTime AddWeeks(this DateTime date, double value)
        {
            return date.AddDays(value*7);
        }

        public static DateTime SubtractWeeks(this DateTime date, double value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddWeeks(value*-1);
        }

        public static DateTime SubtractMonths(this DateTime date, int months)
        {
            if (months < 0)
                throw new ArgumentException("Months cannot be less than 0.", nameof(months));

            return date.AddMonths(months*-1);
        }

        public static DateTime SubtractYears(this DateTime date, int value)
        {
            if (value < 0)
                throw new ArgumentException("Value cannot be less than 0.", nameof(value));

            return date.AddYears(value*-1);
        }

        #endregion
    }
}
