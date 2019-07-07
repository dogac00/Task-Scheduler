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

                UpdateTask(task);

            }, null, 3000, 3000);

            TimerUtils.AddTimer(timer, task.Name, "Task Status Updater", 3000, 3000);
        }

        public static void UpdateTask(Task task)
        {
            if (TaskRunner.IsTaskRunning(task))
            {
                if (task.IsRunning == false)
                {
                    task.IsRunning = true;

                    JsonUtils.UpdateTask(task);
                }
            }
            else
            {
                if (task.IsRunning == true)
                {
                    task.IsRunning = false;
                    task.ProcessId = -1;

                    JsonUtils.UpdateTask(task);
                }
            }
        }

        public static void UpdateStatusForLoaded(Task task)
        {
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (TaskUtils.IsNull(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    return;
                }

                UpdateTask(task);

            }, null, 3000, 3000);

            TimerUtils.AddTimer(timer, task.Name, "Loaded Task Updater", 3000, 3000);
        }

        public static void UpdateStatusConsecutively(Task task)
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
                {
                    TaskUtils.StartDelayForConsecutive(task);
                    TimerUtils.DisposeTimer(timer);
                }

            }, null, 3000, 3000);

            TimerUtils.AddTimer(timer, task.Name, "Consecutive Checker Timer", 3000, 3000);
        }
    }
}
