using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskScheduler
{
    static class TaskUpdater
    {
        private static readonly MainForm Form = MainForm.Form;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void UpdateStatusEverySeconds(Task task)
        {
            Timer timer = null;

            timer = TimerUtils.CreateTimer(() => 
            
                TaskActions.UpdateTasksRunningStatus(timer, task), 3000, 3000);

            TimerUtils.AddTimer(timer, task.Name, "Task Status Updater", 3000, 3000);
        }

        public static void UpdateTask(Task task)
        {
            if (TaskRunner.IsTaskRunning(task))
            {
                if (task.IsRunning == false)
                {
                    logger.Info($"{task} is starting.");

                    task.IsRunning = true;

                    Form.Repository.UpdateTask(task);
                }
            }
            else
            {
                if (task.IsRunning == true)
                {
                    logger.Info($"{task} is stopping.");

                    task.IsRunning = false;
                    task.ProcessId = -1;

                    Form.Repository.UpdateTask(task);
                }
            }
        }

        public static void UpdateStatusForLoaded(Task task)
        {
            Timer timer = null;

            timer = TimerUtils.CreateTimer(() => 
            
                    TaskActions.UpdateForLoadedTask(timer, task), 3000, 3000);

            var taskTimer = TimerUtils.AddTimer(timer, task.Name, "Loaded Task Updater", 3000, 3000);

            logger.Info($"{taskTimer} is added.");
        }

        public static void UpdateStatusConsecutively(Task task)
        {
            Timer timer = null;

            timer = TimerUtils.CreateTimer(() => 

                TaskActions.UpdateStatusForConsecutiveTask(timer, task), 3000, 3000);

            var taskTimer = TimerUtils.AddTimer(timer, task.Name, "Consecutive Checker Timer", 3000, 3000);

            logger.Info($"{taskTimer} is added.");
        }
    }
}
