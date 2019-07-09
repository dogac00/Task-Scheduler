using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
            var startConsecutivelyValue = (float) form.StartConsecutivelyDelay.Value;

            var delay = (long)
                TimeSpanUtils
                .GenerateTimeSpan(startConsecutivelyValue, IntervalUtils.GetInterval())
                .TotalMilliseconds;

            Timer timer = null;

            TimerUtils.CreateTimer(() => 
               
                TaskActions.RunTaskUpdateTaskDisposeTimer(timer, task), delay, Timeout.Infinite);

            TimerUtils.AddTimer(timer, task.Name, "Consecutive Delayer Timer", delay, -1);
        }

        public static void SetTaskStartingTimer(Task task)
        {
            var dueTime = TimeSpanUtils.GetMillisecondsFromNow(task.Period.StartDate);

            Timer timer = null;

            timer = TimerUtils.CreateTimer(() =>

                    TaskActions.StartTaskDisposeTimer(timer, task), dueTime, Timeout.Infinite);

            TimerUtils.AddTimer(timer, task.Name, "Task Starting Timer", dueTime, -1);
        }

        public static bool IsNull(Task task)
        {
            return task == null || form.Repository.IsTaskNull(task);
        }

        public static void UpdateTaskForNotifyEmail(Task task)
        {
            var emails = EmailUtils.GetTextBoxEmailsList();
            var isToBeNotified = true;
            var dontRunLongerThan = form.GetDontRunLongerThanValue();

            var emailInfo = new EmailInfo(isToBeNotified, emails, dontRunLongerThan);

            task.EmailInfo = emailInfo;

            form.Repository.UpdateTask(task);
        }

        public static string GetTaskTimeBetween(Task task)
        {
            return task.Period.TimeBetween == TimeSpan.Zero ?
                    "Just Once." : task.Period.TimeBetween.ToString();
        }
    }
}
