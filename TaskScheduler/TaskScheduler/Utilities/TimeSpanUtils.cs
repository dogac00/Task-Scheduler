using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    class TimeSpanUtils
    {
        public static TimeSpan GenerateTimeSpan(int every, Interval interval)
        {
            return GenerateTimeSpan((float) every, interval);
        }

        public static long GetMillisecondsFromNow(DateTime date)
        {
            var difference = new TimeSpan(date.Ticks - DateTime.Now.Ticks);

            return (long) difference.TotalMilliseconds;
        }

        public static TimeSpan GetDifference(DateTime higherTime, DateTime lowerTime)
        {
            var higher = new TimeSpan(higherTime.Ticks);
            var lower = new TimeSpan(lowerTime.Ticks);

            return higher - lower;
        }

        public static TimeSpan GenerateTimeSpan(float every, Interval interval)
        {
            if (interval == Interval.Week)
            {
                return TimeSpan.FromDays(7).Multiply(every);
            }
            else if (interval == Interval.Day)
            {
                return TimeSpan.FromDays(every);
            }
            else if (interval == Interval.Hour)
            {
                return TimeSpan.FromHours(every);
            }
            else
            {
                return TimeSpan.FromMinutes(every);
            }
        }
    }
}
