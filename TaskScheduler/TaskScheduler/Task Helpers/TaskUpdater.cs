using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskScheduler
{
    static class TaskUpdater
    {
        private static readonly Form1 Form = Form1.Form;

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
                    task.IsRunning = true;

                    Form.Repository.UpdateTask(task);
                }
            }
            else
            {
                if (task.IsRunning == true)
                {
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

            TimerUtils.AddTimer(timer, task.Name, "Loaded Task Updater", 3000, 3000);
        }

        public static void UpdateStatusConsecutively(Task task)
        {
            Timer timer = null;

            timer = TimerUtils.CreateTimer(() => 

                TaskActions.UpdateStatusForConsecutiveTask(timer, task), 3000, 3000);

            TimerUtils.AddTimer(timer, task.Name, "Consecutive Checker Timer", 3000, 3000);
        }
    }
}
