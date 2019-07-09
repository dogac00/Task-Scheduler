using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    public static class TimeSpanExtensions
    {
        public static TimeSpan Multiply(this TimeSpan multiplicand, int multiplier)
        {
            return TimeSpan.FromTicks(multiplicand.Ticks * multiplier);
        }

        public static TimeSpan Multiply(this TimeSpan multiplicand, float multiplier)
        {
            return TimeSpan.FromTicks((long)(multiplicand.Ticks * multiplier));
        }

        public static TimeSpan Multiply(this TimeSpan multiplicand, double multiplier)
        {
            return TimeSpan.FromTicks((long)(multiplicand.Ticks * multiplier));
        }
    }
}
