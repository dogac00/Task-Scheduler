using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskScheduler
{
    static class TaskStarter
    {
        public static void StartTaskAccordingly(Task task)
        {
            switch (task.Period.Property)
            {
                case StartProperty.Once:
                    StartTaskOnce(task);
                    break;

                case StartProperty.Periodically:
                    StartTaskPeriodically(task);
                    break;

                case StartProperty.Consecutively:
                    StartTaskConsecutively(task);
                    break;
            }
        }

        public static void StartTaskOnce(Task task)
        {
            TaskRunner.RunTask(task);
            TaskUpdater.UpdateStatusEverySeconds(task);
        }

        public static void StartTaskPeriodically(Task task)
        {
            TaskRunner.RunTaskPeriodically(task);
            TaskUpdater.UpdateStatusEverySeconds(task);
        }

        public static void StartTaskConsecutively(Task task)
        {
            TaskRunner.RunTask(task);
            TaskUpdater.UpdateStatusConsecutively(task);
            TaskUpdater.UpdateStatusEverySeconds(task);
        }

        public static void StartNotificationTimer(Task task)
        {
            var taskStartingDate = task.Period.StartDate;
            var dontRunLongerThanValue = Form1.Form.GetDontRunLongerThanValue();

            var dueTime = TimeSpanUtils.GetMillisecondsFromNow(taskStartingDate);

            if (dueTime < 0) dueTime = 0;

            bool tryToSend = false;

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (TaskUtils.IsNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                if (TimeSpanUtils
                .GetDifference(DateTime.Now, taskStartingDate) > dontRunLongerThanValue)
                {
                    if (!tryToSend && TaskRunner.IsTaskRunning(task))
                    {
                        tryToSend = true;
                        EmailUtils.RedirectToSendEmail(task.Name, dontRunLongerThanValue);
                    }

                    TimerUtils.DisposeTimer(timer);
                }

            }, null, dueTime, 3000);

            Form1.Form.Timers.Add(timer);
        }
    }
}
