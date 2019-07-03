using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    class Validation
    {
        private readonly Form1 form;

        public Validation(Form form)
        {
            this.form = (Form1) form;
        }

        public bool IsValidForNonClick()
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

        public bool IsValidForNames()
        {
            if (form.TaskName.Text == "" || form.TaskExecutablePath.Text == "")
            {
                MessageBox.Show("Please enter task name and task exe path.");
                return false;
            }

            return true;
        }

        public bool IsValidForEmail()
        {
            if (form.NotifyButton.Checked)
            {
                if (form.EmailAddressTextBox.Text == "")
                {
                    MessageBox.Show("Please enter email.");
                    return false;
                }
                else if (!IsValidEmail(form.EmailAddressTextBox.Text))
                {
                    MessageBox.Show("Please enter a valid email.");
                    return false;
                }
            }

            return true;
        }

        public bool IsValidNumericUpDown()
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

        public bool IsTaskNameValid()
        {
            if (JsonUtils.GetTaskByName(form.TaskName.Text) != null)
            {
                MessageBox.Show("Task name exists. Please select another name.");
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
    }
}
