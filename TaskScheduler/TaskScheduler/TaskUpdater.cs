using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class TaskUpdater
    {
        public static void UpdateStatusEverySeconds(Task task)
        {
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (TaskUtils.IsNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                if (!TaskRunner.IsTaskRunning(task))
                    TaskUtils.DisposeIfOnce(timer, task);

            }, null, 3000, 3000);

            Form1.Form.Timers.Add(timer);
        }

        public static void UpdateStatusConsecutively(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.FromSeconds(3);
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (TaskUtils.IsNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }
                if (!TaskRunner.IsTaskRunning(task))
                {
                    TaskUtils.StartDelayForConsecutive(task);
                    TimerUtils.DisposeTimer(timer);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }
    }
}
