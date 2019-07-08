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
            if (task == null)
                return false;

            try
            {
                task.ProcessId = Process.Start(task.ExecutablePath).Id;
                task.IsRunning = true;

                JsonUtils.UpdateTask(task);

                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {

                MessageBox.Show("Invalid executable path.");
                JsonUtils.DeleteTask(task);

                return false;
            }
        }

        public static bool IsTaskRunning(Task task)
        {
            if (TaskUtils.IsNull(task))
                return false;

            return ProcessUtils.IsProcessRunning(task);
        }
    }
}
