using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class IntervalUtils
    {
        private static readonly Form1 form = Form1.Form;

        public static Interval GetDontRunLongerThanInterval()
        {
            if (form.RunsLongerThanWeek.Checked)
                return Interval.Week;
            else if (form.RunsLongerThanDay.Checked)
                return Interval.Day;
            else if (form.RunsLongerThanHour.Checked)
                return Interval.Hour;
            else
                return Interval.Min;
        }

        public static Interval GetInterval()
        {
            if (form.StartPeriodicallyButton.Checked)
            {
                if (form.StartPeriodicallyWeek.Checked)
                    return Interval.Week;
                else if (form.StartPeriodicallyDay.Checked)
                    return Interval.Day;
                else if (form.StartPeriodicallyHour.Checked)
                    return Interval.Hour;
                else
                    return Interval.Min;
            }
            else
            {
                if (form.StartConsecutivelyWeek.Checked)
                    return Interval.Week;
                else if (form.StartConsecutivelyDay.Checked)
                    return Interval.Day;
                else if (form.StartConsecutivelyHour.Checked)
                    return Interval.Hour;
                else
                    return Interval.Min;
            }
        }
    }
}
