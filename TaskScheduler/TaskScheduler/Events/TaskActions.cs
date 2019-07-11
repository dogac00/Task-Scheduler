using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskScheduler
{
    static class TaskActions
    {
        public static void RunTaskDisposeIfNull(Timer timer, Task task)
        {
            if (TaskUtils.IsNull(task))
            {
                TimerUtils.DisposeTimer(timer);
                return;
            }

            TaskRunner.RunTask(task);
        }

        public static void UpdateTasksRunningStatus(Timer timer, Task task)
        {
            if (TaskUtils.IsNull(task))
            {
                TimerUtils.DisposeTimer(timer);
                return;
            }

            TaskUpdater.UpdateTask(task);
        }

        public static void UpdateForLoadedTask(Timer timer, Task task)
        {
            if (TaskUtils.IsNull(task))
            {
                TimerUtils.DisposeTimer(timer);
                return;
            }

            TaskUpdater.UpdateTask(task);
        }

        public static void UpdateStatusForConsecutiveTask(Timer timer, Task task)
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
        }

        public static void RunTaskUpdateTaskDisposeTimer(Timer timer, Task task)
        {
            TaskRunner.RunTask(task);
            TaskUpdater.UpdateStatusConsecutively(task);
            TimerUtils.DisposeTimer(timer);
        }

        public static void RunTaskUpdateForConsecutiveDisposeTimer(Timer timer, Task task)
        {
            TaskRunner.RunTask(task);
            TaskUpdater.UpdateStatusConsecutively(task);
            TimerUtils.DisposeTimer(timer);
        }

        public static void StartTaskThenDisposeTimer(Timer timer, Task task)
        {
            if (!TaskUtils.IsNull(task))
            {
                TaskStarter.StartTaskAccordingly(task);
            }

            TimerUtils.DisposeTimer(timer);
        }
    }
}
