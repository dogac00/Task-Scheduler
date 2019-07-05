using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class TaskUtils
    {
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

        public static void StartDelayForConsecutive(Task task)
        {
            var delay = TimeSpanUtils
                .GenerateTimeSpan((float)Form1.Form.StartConsecutivelyDelay.Value,
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

                TaskStarter.StartTaskAccordingly(task);
                TimerUtils.DisposeTimer(timer);

            }, null, startTimeSpan, twentyDays);

            Form1.Form.Timers.Add(timer);
        }

        public static void CleanUpAndDispose(Task task, System.Threading.Timer timer)
        {
            DeleteTask(task);
            TimerUtils.DisposeTimer(timer);
        }

        public static void DeleteTask(Task task)
        {
            DeleteTaskInMemory(task);
            JsonUtils.DeleteTask(task);
        }

        public static void DeleteTaskInMemory(Task task)
        {
            task = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public static string GetTaskTimeBetween(Task task)
        {
            return task.Period.TimeBetween == TimeSpan.Zero ?
                    "Just Once." : task.Period.TimeBetween.ToString();
        }
    }
}
