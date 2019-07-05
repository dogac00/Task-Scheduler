using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                if (task == null || JsonUtils.IsTaskNull(task))
                {
                    TaskUtils.CleanUpAndDispose(task, timer);

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
            catch (Exception e)
            {

                MessageBox.Show(e.Message);

                JsonUtils.DeleteTask(task);

                return false;
            }
        }
        public static bool IsTaskRunning(Task task)
        {
            if (task == null)
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
