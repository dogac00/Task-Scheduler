using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    class TaskPeriodUtils
    {
        public static TaskPeriod SetPeriod()
        {
            TaskPeriod period;

            if (MainForm.Form.StartOnceButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Once,
                    StartDate = MainForm.Form.GetStartDate(StartProperty.Once),
                    TimeBetween = TimeSpan.FromSeconds(0)
                };
            }
            else if (MainForm.Form.StartPeriodicallyButton.Checked)
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Periodically,
                    StartDate = MainForm.Form.GetStartDate(StartProperty.Periodically),
                    TimeBetween = GetTimeBetween(StartProperty.Periodically)
                };
            }
            else
            {
                period = new TaskPeriod
                {
                    Property = StartProperty.Consecutively,
                    StartDate = MainForm.Form.GetStartDate(StartProperty.Consecutively),
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
                every = (float) MainForm.Form.StartPeriodicallyEvery.Value;
                interval = IntervalUtils.GetInterval();
            }
            else
            {
                every = (float) MainForm.Form.StartConsecutivelyDelay.Value;
                interval = IntervalUtils.GetInterval();
            }

            return TimeSpanUtils.GenerateTimeSpan(every, interval);
        }
    }
}
