using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class TaskPeriodUtils
    {
        public static TaskPeriod SetPeriod()
        {
            TaskPeriod period;

            if (Form1.Form.StartOnceButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Once,
                    StartDate = Form1.Form.GetStartDate(StartProperty.Once),
                    TimeBetween = TimeSpan.FromSeconds(0)
                };
            }
            else if (Form1.Form.StartPeriodicallyButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Periodically,
                    StartDate = Form1.Form.GetStartDate(StartProperty.Periodically),
                    TimeBetween = GetTimeBetween(StartProperty.Periodically)
                };
            }
            else
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Consecutively,
                    StartDate = Form1.Form.GetStartDate(StartProperty.Consecutively),
                    TimeBetween = GetTimeBetween(StartProperty.Consecutively)
                };
            }

            return period;
        }

        public static TimeSpan GetTimeBetween(StartProperty prop)
        {
            float every;
            Interval interval;

            if (prop == StartProperty.Periodically)
            {
                every = (float) Form1.Form.StartPeriodicallyEvery.Value;
                interval = IntervalUtils.GetInterval();
            }
            else
            {
                every = (float) Form1.Form.StartConsecutivelyDelay.Value;
                interval = IntervalUtils.GetInterval();
            }

            return TimeSpanUtils.GenerateTimeSpan(every, interval);
        }
    }
}
