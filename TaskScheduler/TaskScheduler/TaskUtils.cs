using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class TaskUtils
    {
        public static void RunTaskPeriodically(Task task)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = task.Period.TimeBetween;

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                RunTask(task);

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        public static void StartNotificationTimer(Task task, int everySeconds, 
            TimeSpan dontRunLongerThan)
        {
            var taskStartingDate = task.Period.StartDate;
            TimeSpan startTimeSpan = new TimeSpan(taskStartingDate.Ticks - DateTime.Now.Ticks);

            if (startTimeSpan < TimeSpan.Zero) startTimeSpan = TimeSpan.Zero;

            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            bool isEmailSent = false;
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                if (TimeSpanUtils
                .GetDifference(DateTime.Now, taskStartingDate) > dontRunLongerThan)
                {
                    if (!isEmailSent && IsTaskRunning(task))
                    {
                        isEmailSent = true;
                        EmailUtils.SendEmail(task.Name, dontRunLongerThan);
                    }

                    TimerUtils.DisposeTimer(timer);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        public static void StartDelayForConsecutive(Task task)
        {
            var delay = TimeSpanUtils
                .GenerateTimeSpan((float)Form1.Form.StartConsecutivelyDelay.Value,
                                    Form1.Form.GetInterval());
            var twentyDays = TimeSpan.FromDays(25);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                StartTaskConsecutively(task);
                TimerUtils.DisposeTimer(timer);

            }, null, delay, twentyDays);

            Form1.Form.Timers.Add(timer);
        }

        public static Task CreateTask()
        {
            Task task = new Task
            {
                Id = -1,
                Name = Form1.Form.TaskName.Text,
                ExecutablePath = Form1.Form.TaskExecutablePath.Text,
                IsRunning = false,
                ProcessId = -1,
                Period = TaskPeriodUtils.SetPeriod(),
                EmailInfo = EmailUtils.SetEmailInfo()
            };

            return task;
        }

        public static void StartTaskPeriodically(Task task)
        {
            RunTaskPeriodically(task);
            UpdateStatusEverySeconds(task, 3);
        }

        public static void StartTaskConsecutively(Task task)
        {
            RunTask(task);
            UpdateStatusConsecutively(task, 3);
            UpdateStatusEverySeconds(task, 3);
        }

        public static bool RunTask(Task task)
        {
            if (task == null)
                return false;

            try
            {
                task.ProcessId = Process.Start(task.ExecutablePath).Id;
                task.IsRunning = true;

                JsonUtils.UpdateTask(task);

                return true;
            }
            catch (Exception e) {

                MessageBox.Show(e.Message);

                JsonUtils.DeleteTask(task);

                return false;
            }
        }

        public static void UpdateStatusEverySeconds(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.FromSeconds(3);
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                if (!IsTaskRunning(task))
                    DisposeIfOnce(timer, task);

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        private static void DisposeIfOnce(System.Threading.Timer timer, Task task)
        {
            if (task.Period.Property == StartProperty.Once)
                TimerUtils.DisposeTimer(timer);
        }

        public static void UpdateStatusConsecutively(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }
                if (!IsTaskRunning(task))
                {
                    StartDelayForConsecutive(task);
                    TimerUtils.DisposeTimer(timer);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        private static bool IsTaskRunning(Task task)
        {
            if (task == null)
                return false;

            if (ProcessUtils.IsProcessRunning(task))
            {
                task.IsRunning = true;

                JsonUtils.UpdateTask(task);

                return true;
            }
            else
            {
                task.IsRunning = false;
                task.ProcessId = -1;

                JsonUtils.UpdateTask(task);

                return false;
            }
        }

        public static void SetTaskStartingTimer(Task task)
        {
            var startTimeSpan = new TimeSpan(task.Period.StartDate.Ticks - DateTime.Now.Ticks);
            var twentyDays = TimeSpan.FromDays(20);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                StartTaskAccordingly(task);
                TimerUtils.DisposeTimer(timer);

            }, null, startTimeSpan, twentyDays);

            Form1.Form.Timers.Add(timer);
        }

        private static void StartTaskAccordingly(Task task)
        {
            switch (task.Period.Property)
            {
                case StartProperty.Once:
                    RunTask(task);
                    UpdateStatusEverySeconds(task, 3);
                    break;

                case StartProperty.Periodically:
                    StartTaskPeriodically(task);
                    break;

                case StartProperty.Consecutively:
                    StartTaskConsecutively(task);
                    break;
            }
        }
    }
}
