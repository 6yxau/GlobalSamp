using System;

namespace GlobalSamp.Tools.Utils
{
    public static class DateUtils
    {
        private static readonly TimeSpan _epoch = new TimeSpan(new DateTime(1970, 1, 1).Ticks);

        public static double GetTimestamp()
        {
            return (new TimeSpan(DateTime.UtcNow.Ticks) - _epoch).TotalMilliseconds;
        }
    }
}