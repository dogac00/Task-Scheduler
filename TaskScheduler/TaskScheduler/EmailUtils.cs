﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class EmailUtils
    {
        public static EmailInfo SetEmailInfo()
        {
            if (Form1.Form.NotifyButton.Checked)
            {
                float every = (float) Form1.Form.RunsLongerThanEvery.Value;
                var interval = GetSelectedRunsLongerThanInterval();

                EmailInfo emailInfo = new EmailInfo
                {
                    IsToBeNotified = true,
                    EmailAddress = Form1.Form.EmailAddressTextBox.Text,
                    NoLongerThan = TimeSpanUtils.GenerateTimeSpan(every, interval)
                };

                return emailInfo;
            }
            else
            {
                return null;
            }
        }

        private static Interval GetSelectedRunsLongerThanInterval()
        {
            if (Form1.Form.RunsLongerThanWeek.Checked)
                return Interval.Week;
            else if (Form1.Form.RunsLongerThanDay.Checked)
                return Interval.Day;
            else if (Form1.Form.RunsLongerThanHour.Checked)
                return Interval.Hour;
            else
                return Interval.Min;
        }
    }
}