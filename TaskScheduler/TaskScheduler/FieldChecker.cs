using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class FieldChecker
    {
        private static readonly Form1 form = Form1.Form;

        public static void CheckFields(Task task)
        {
            try
            {
                CheckForStartOnce(task);
                CheckForStartPeriodically(task);
                CheckForStartConsecutively(task);
                CheckForNotifyEmail(task);

                MessageBox.Show("Task is successfully added.");
            }
            catch (Exception e) {

                TaskUtils.DeleteTask(task);
                MessageBox.Show(e.Message);

            }
        }

        private static void CheckForStartOnce(Task task)
        {
            if (task.Period.Property == StartProperty.Once)
            {
                if (form.StartOnceNowButton.Checked)
                {
                    if (TaskRunner.RunTask(task))
                    {
                        TaskUpdater.UpdateStatusEverySeconds(task, 3);
                    }
                }
                else if (form.StartOnceSelectDateButton.Checked)
                {
                    TaskUtils.SetTaskStartingTimer(task);
                }
            }
        }

        private static void CheckForStartPeriodically(Task task)
        {
            if (task.Period.Property == StartProperty.Periodically)
            {
                if (form.StartPeriodicallyNowButton.Checked)
                {
                    TaskStarter.StartTaskPeriodically(task);
                }
                else if (form.StartPeriodicallySelectDateButton.Checked)
                {
                    TaskUtils.SetTaskStartingTimer(task);
                }
            }
        }

        private static void CheckForStartConsecutively(Task task)
        {
            if (task.Period.Property == StartProperty.Consecutively)
            {
                if (form.StartConsecutivelyNowButton.Checked)
                {
                    TaskStarter.StartTaskConsecutively(task);
                }
                else if (form.StartConsecutivelySelectDateButton.Checked)
                {
                    TaskUtils.SetTaskStartingTimer(task);
                }
            }
        }

        private static void CheckForNotifyEmail(Task task)
        {
            if (form.NotifyButton.Checked)
            {
                JsonUtils.UpdateForNotified(task);

                var dontRunLongerThan = form.GetDontRunLongerThanValue();

                TaskStarter.StartNotificationTimer(task, 3, dontRunLongerThan);
            }
        }
    }
}
