using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class Validation
    {
        private static readonly Form1 form = Form1.Form;

        public static bool IsValid
        {
            get
            {
                if (!IsValidForNames()) return false;
                if (!IsValidForNonClick()) return false;
                if (!IsValidForEmail()) return false;
                if (!IsValidNumericUpDown()) return false;
                if (!IsTaskNameValid()) return false;
                if (!IsValidForDates()) return false;
                if (!IsValidForTimeBetween()) return false;

                return true;
            }
        }

        private static bool IsValidForNonClick()
        {
            if (form.StartOnceButton.Checked || form.StartPeriodicallyButton.Checked
                || form.StartConsecutivelyButton.Checked)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please select a starting value.");
                return false;
            }
        }

        private static bool IsValidForNames()
        {
            if (form.TaskName.Text == "" || form.TaskExecutablePath.Text == "")
            {
                MessageBox.Show("Please enter task name and task exe path.");
                return false;
            }

            return true;
        }

        private static bool IsValidForEmail()
        {
            if (form.NotifyButton.Checked)
            {
                var emailTextBox = form.EmailAddressTextBox.Text;

                if (emailTextBox == "")
                {
                    MessageBox.Show("Please enter email.");
                    return false;
                }

                var trimmedEmails = emailTextBox.RemoveAllWhiteSpace();

                if (trimmedEmails == "")
                {
                    MessageBox.Show("Please enter email.");
                    return false;
                }

                var emails = trimmedEmails.Split(',');

                if (!IsValidEmails(emails))
                {
                    MessageBox.Show("All emails must be valid.");
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidEmails(string [] emails)
        {
            foreach (var email in emails)
                if (!IsValidEmail(email))
                    return false;

            return true;
        }

        private static bool IsValidForTimeBetween()
        {
            if (!ArePeriodsOrDelaysValid())
            {
                MessageBox.Show("Period or delay must not exceed 50 days.");
                return false;
            }

            if (!IsNotifyLongerThanValid())
            {
                MessageBox.Show("Don't run longer than value must not exceed 50 days.");
                return false;
            }

            return true;
        }

        private static bool ArePeriodsOrDelaysValid()
        {
            if (form.StartPeriodicallyButton.Checked || form.StartConsecutivelyButton.Checked)
            {
                var periodBetween = TaskPeriodUtils.SetPeriod().TimeBetween;

                if (IsTimeSpanExceedingLimit(periodBetween))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsNotifyLongerThanValid()
        {
            if (form.NotifyButton.Checked)
            {
                var dontRunLongerThan = form.GetDontRunLongerThanValue();

                if (IsTimeSpanExceedingLimit(dontRunLongerThan))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidNumericUpDown()
        {
            if (form.StartPeriodicallyButton.Checked && 
                form.StartPeriodicallyEvery.Value == 0M)
            {
                MessageBox.Show("Period value cannot be zero.");
                return false;
            }
            else if (form.StartConsecutivelyButton.Checked &&
                    form.StartConsecutivelyDelay.Value == 0M)
            {
                MessageBox.Show("Delay value cannot be zero.");
                return false;
            }
            else if (form.NotifyButton.Checked &&
                    form.RunsLongerThanEvery.Value == 0M)
            {
                MessageBox.Show("Longer than value cannot be zero.");
                return false;
            }

            return true;
        }

        private static bool IsTaskNameValid()
        {
            if (JsonUtils.GetTaskByName(form.TaskName.Text) != null)
            {
                MessageBox.Show("Task name exists. Please select another name.");
                return false;
            }

            return true;
        }

        private static bool IsValidForDates()
        {
            return IsValidForDateTimePickers();
        }

        private static bool IsValidForDateTimePickers()
        {
            if (form.StartOnceButton.Checked && form.StartOnceSelectDateButton.Checked)
            {
                if (!IsDateValid(form.StartOnceDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(form.StartOnceDateTimePicker.Value)) return false;
            }
            else if (form.StartPeriodicallyButton.Checked && form.StartPeriodicallySelectDateButton.Checked)
            {
                if (!IsDateValid(form.StartPeriodicallyDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(form.StartPeriodicallyDateTimePicker.Value)) return false;
            }
            else if (form.StartConsecutivelyButton.Checked && form.StartConsecutivelySelectDateButton.Checked)
            {
                if (!IsDateValid(form.StartConsecutivelyDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(form.StartConsecutivelyDateTimePicker.Value)) return false;
            }

            return true;
        }

        private static bool IsDateTimeSpanExceedingLimit(DateTime date)
        {
            var difference = TimeSpanUtils.GetDifference(date, DateTime.Now);

            return IsTimeSpanExceedingLimit(difference);
        }

        private static bool IsTimeSpanExceedingLimit(TimeSpan timeSpan)
        {
            return timeSpan > TimeSpan.FromDays(49);
        }

        private static bool IsDateTimeSpanValid(DateTime date)
        {
            if (IsDateTimeSpanExceedingLimit(date))
            {
                MessageBox.Show("Task starting date can not be longer than 50 days.");
                return false;
            }

            return true;
        }

        private static bool IsDateValid(DateTime date)
        {
            if (IsDatePassedNow(date))
            {
                MessageBox.Show("Date can not be passed now.");
                return false;
            }

            return true;
        }

        private static bool IsDatePassedNow(DateTime date)
        {
            return date < DateTime.Now;
        }

        private static bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
    }
}
