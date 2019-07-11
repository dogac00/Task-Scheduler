using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class Validation
    {
        private static readonly MainForm Form = MainForm.Form;

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
                if (!IsValidForExePath()) return false;

                return true;
            }
        }

        private static bool IsValidForNonClick()
        {
            if (Form.StartOnceButton.Checked || Form.StartPeriodicallyButton.Checked
                || Form.StartConsecutivelyButton.Checked)
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
            if (Form.TaskName.Text == "" || Form.TaskExecutablePath.Text == "")
            {
                MessageBox.Show("Please enter task name and task exe path.");
                return false;
            }

            return true;
        }

        private static bool IsValidForExePath()
        {
            if (System.IO.File.Exists(Form.TaskExecutablePath.Text))
                return true;

            if (IsValidURL(Form.TaskExecutablePath.Text))
                return true;

            MessageBox.Show("Executable path is invalid.");

            return false;
        }

        private static bool IsValidForEmail()
        {
            if (Form.NotifyButton.Checked)
            {
                var emailTextBox = Form.EmailAddressTextBox.Text;

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
            if (Form.StartPeriodicallyButton.Checked || Form.StartConsecutivelyButton.Checked)
            {
                var periodBetween = TaskPeriodUtils.SetPeriod().TimeBetween;

                if (IsTimeSpanExceedingLimit(periodBetween))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidURL(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private static bool IsNotifyLongerThanValid()
        {
            if (Form.NotifyButton.Checked)
            {
                var dontRunLongerThan = Form.GetDontRunLongerThanValue();

                if (IsTimeSpanExceedingLimit(dontRunLongerThan))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsValidNumericUpDown()
        {
            if (Form.StartPeriodicallyButton.Checked && 
                Form.StartPeriodicallyEvery.Value == 0M)
            {
                MessageBox.Show("Period value cannot be zero.");
                return false;
            }
            else if (Form.StartConsecutivelyButton.Checked &&
                    Form.StartConsecutivelyDelay.Value == 0M)
            {
                MessageBox.Show("Delay value cannot be zero.");
                return false;
            }
            else if (Form.NotifyButton.Checked &&
                    Form.RunsLongerThanEvery.Value == 0M)
            {
                MessageBox.Show("Longer than value cannot be zero.");
                return false;
            }

            return true;
        }

        private static bool IsTaskNameValid()
        {
            if (Form.Repository.GetTaskByName(Form.TaskName.Text) != null)
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
            if (Form.StartOnceButton.Checked && Form.StartOnceSelectDateButton.Checked)
            {
                if (!IsDateValid(Form.StartOnceDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(Form.StartOnceDateTimePicker.Value)) return false;
            }
            else if (Form.StartPeriodicallyButton.Checked && Form.StartPeriodicallySelectDateButton.Checked)
            {
                if (!IsDateValid(Form.StartPeriodicallyDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(Form.StartPeriodicallyDateTimePicker.Value)) return false;
            }
            else if (Form.StartConsecutivelyButton.Checked && Form.StartConsecutivelySelectDateButton.Checked)
            {
                if (!IsDateValid(Form.StartConsecutivelyDateTimePicker.Value)) return false;

                if (!IsDateTimeSpanValid(Form.StartConsecutivelyDateTimePicker.Value)) return false;
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
