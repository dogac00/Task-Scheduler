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
            if (!IsValidForDateTimePickers())
            {
                MessageBox.Show("Please select a valid date.");
                return false;
            }

            return true;
        }

        private static bool IsValidForDateTimePickers()
        {
            if (form.StartOnceButton.Checked && form.StartOnceSelectDateButton.Checked)
            {
                return !IsDatePassed(form.StartOnceDateTimePicker.Value);
            }
            else if (form.StartPeriodicallyButton.Checked && form.StartPeriodicallySelectDateButton.Checked)
            {
                return !IsDatePassed(form.StartPeriodicallyDateTimePicker.Value);
            }
            else if (form.StartConsecutivelyButton.Checked && form.StartConsecutivelySelectDateButton.Checked)
            {
                return !IsDatePassed(form.StartConsecutivelyDateTimePicker.Value);
            }

            return true;
        }

        private static bool IsDatePassed(DateTime date)
        {
            return date < DateTime.Now;
        }

        private static bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
    }
}
