using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskScheduler
{
    static class TaskRunner
    {
        private static readonly MainForm Form = MainForm.Form;

        public static void RunTaskPeriodically(Task task)
        {
            var dueTime = TimeSpan.Zero;
            var period = task.Period.TimeBetween;

            System.Threading.Timer timer = null;

            timer = TimerUtils.CreateTimer(() => 
                    
                    TaskActions.RunTaskDisposeIfNull(timer, task), dueTime, period);

            TimerUtils.AddTimer(timer, task.Name, "Periodical Runner Timer", dueTime, period);
        }

        public static bool RunTask(Task task)
        {
            if (task == null) return false;

            Process process = Process.Start(task.ExecutablePath);

            if (process == null) return false;

            task.ProcessId = process.Id;
            task.IsRunning = true;

            Form.Repository.UpdateTask(task);

            return true;
        }

        public static bool IsTaskRunning(Task task)
        {
            if (TaskUtils.IsNull(task))
                return false;

            return ProcessUtils.IsProcessRunning(task);
        }
    }
}
