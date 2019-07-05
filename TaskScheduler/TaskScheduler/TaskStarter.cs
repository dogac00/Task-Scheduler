using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class TaskStarter
    {
        public static void StartTaskAccordingly(Task task)
        {
            switch (task.Period.Property)
            {
                case StartProperty.Once:
                    TaskRunner.RunTask(task);
                    TaskUpdater.UpdateStatusEverySeconds(task, 3);
                    break;

                case StartProperty.Periodically:
                    StartTaskPeriodically(task);
                    break;

                case StartProperty.Consecutively:
                    StartTaskConsecutively(task);
                    break;
            }
        }

        public static void StartTaskPeriodically(Task task)
        {
            TaskRunner.RunTaskPeriodically(task);
            TaskUpdater.UpdateStatusEverySeconds(task, 3);
        }

        public static void StartTaskConsecutively(Task task)
        {
            TaskRunner.RunTask(task);
            TaskUpdater.UpdateStatusConsecutively(task, 3);
            TaskUpdater.UpdateStatusEverySeconds(task, 3);
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
                    if (!isEmailSent && TaskRunner.IsTaskRunning(task))
                    {
                        isEmailSent = true;
                        EmailUtils.SendEmail(task.Name, dontRunLongerThan);
                    }

                    TimerUtils.DisposeTimer(timer);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }
    }
}
