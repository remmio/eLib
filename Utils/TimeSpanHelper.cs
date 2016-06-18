using System;
using System.Collections.Generic;
using System.Linq;

namespace eLib.Utils
{
    public static class TimeSpanHelper
    {
        public static TimeSpan Sum(this IEnumerable<TimeSpan> timeSpans)
        {
            var spans = timeSpans as IList<TimeSpan> ?? timeSpans.ToList();
            return !spans.Any() ? TimeSpan.Zero : spans.Aggregate((current, timeSpan) => current.Add(timeSpan));
        }
    }
}
