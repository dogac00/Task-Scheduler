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
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = task.Period.TimeBetween;

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (TaskUtils.IsNull(task))
                {
                    TimerUtils.DisposeTimer(timer);

                    return;
                }

                RunTask(task);

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
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
            catch (FileNotFoundException)
            {

                MessageBox.Show("Invalid executable path.");
                TaskUtils.DeleteTask(task);

                return false;
            }
        }
        public static bool IsTaskRunning(Task task)
        {
            if (TaskUtils.IsNull(task))
                return false;

            if (ProcessUtils.IsProcessRunning(task))
            {
                task.IsRunning = true;

                JsonUtils.UpdateTask(task);

                return true;
            }
            else
            {
                task.IsRunning = false;
                task.ProcessId = -1;

                JsonUtils.UpdateTask(task);

                return false;
            }
        }
    }
}
