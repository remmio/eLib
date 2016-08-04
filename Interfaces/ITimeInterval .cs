using System;

namespace eLib.Interfaces
{
    public interface ITimeInterval
    {
        TimeSpan StartTime { get; set; }

        TimeSpan EndTime { get; set; }
    }
}
