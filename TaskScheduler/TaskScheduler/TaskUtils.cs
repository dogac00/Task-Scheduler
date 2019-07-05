using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class TaskUtils
    {
        private static readonly Form1 form = Form1.Form;

        public static Task CreateTask()
        {
            Task task = new Task
            {
                Id = -1,
                Name = form.TaskName.Text,
                ExecutablePath = form.TaskExecutablePath.Text,
                IsRunning = false,
                ProcessId = -1,
                Period = TaskPeriodUtils.SetPeriod(),
                EmailInfo = EmailUtils.SetEmailInfo()
            };

            return task;
        }

        public static void StartDelayForConsecutive(Task task)
        {
            var delay = TimeSpanUtils
                .GenerateTimeSpan((float) form.StartConsecutivelyDelay.Value,
                                    IntervalUtils.GetInterval());
            var twentyDays = TimeSpan.FromDays(25);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                TaskStarter.StartTaskConsecutively(task);
                TimerUtils.DisposeTimer(timer);

            }, null, delay, twentyDays);

            Form1.Form.Timers.Add(timer);
        }

        public static void DisposeIfOnce(System.Threading.Timer timer, Task task)
        {
            if (task.Period.Property == StartProperty.Once)
                TimerUtils.DisposeTimer(timer);
        }

        public static void SetTaskStartingTimer(Task task)
        {
            var dueTime = TimeSpanUtils.GetMillisecondsFromNow(task.Period.StartDate);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (!IsNull(task))
                {
                    TaskStarter.StartTaskAccordingly(task);
                }

                TimerUtils.DisposeTimer(timer);

            }, null, dueTime, System.Threading.Timeout.Infinite);

            Form1.Form.Timers.Add(timer);
        }

        public static bool IsNull(Task task)
        {
            return task == null || JsonUtils.IsTaskNull(task);
        }

        public static void CleanUpAndDispose(Task task, System.Threading.Timer timer)
        {
            task = null;

            TimerUtils.DisposeTimer(timer);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void DeleteTask(Task task)
        {
            JsonUtils.DeleteTask(task);
            task = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static void UpdateTaskForNotifyEmail(Task task)
        {
            var emails = EmailUtils.GetTextBoxEmailsList();
            var isToBeNotified = true;
            var dontRunLongerThan = form.GetDontRunLongerThanValue();

            var emailInfo = new EmailInfo(isToBeNotified, emails, dontRunLongerThan);

            task.EmailInfo = emailInfo;

            JsonUtils.UpdateTask(task);
        }

        public static string GetTaskTimeBetween(Task task)
        {
            return task.Period.TimeBetween == TimeSpan.Zero ?
                    "Just Once." : task.Period.TimeBetween.ToString();
        }
    }
}
