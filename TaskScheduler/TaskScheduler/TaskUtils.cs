using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class TaskUtils
    {
        public static void SetTaskTimerForPeriods(Task task)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = task.Period.TimeBetween;

            var timer = new System.Threading.Timer((e) =>
            {

                if (!task.IsRunning)
                {
                    // TimeBetween reached. Run task if not running.
                    RunTask(task);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        public static Task CreateTask()
        {
            Task task = new Task
            {
                Name = Form1.Form.TaskName.Text,
                ExecutablePath = Form1.Form.TaskExecutablePath.Text,
                IsRunning = false,
                Period = TaskPeriodUtils.SetPeriod(),
                EmailInfo = EmailUtils.SetEmailInfo()
            };

            return task;
        }

        public static void StartTask(Task task)
        {
            RunTask(task);
            UpdateStatusEverySeconds(task, 5);
            SetTaskTimerForPeriods(task);
        }

        public static void RunTask(Task task)
        {
            task.IsRunning = true;
            JsonUtils.UpdateTask(task, true);
            Process.Start(task.ExecutablePath);
        }

        public static void UpdateStatusEverySeconds(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.FromSeconds(3);
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            var timer = new System.Threading.Timer((e) =>
            {
                if (!IsProcessRunning(task))
                {
                    if (task.IsRunning != false)
                    {
                        task.IsRunning = false;
                        JsonUtils.UpdateTask(task, false);
                    }
                }
                else
                {
                    if (task.IsRunning != true)
                    {
                        task.IsRunning = true;
                        JsonUtils.UpdateTask(task, true);
                    }
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        public static void SetTaskStartingTimer(Task task, int runEverySeconds)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(runEverySeconds);

            var timer = new System.Threading.Timer((e) =>
            {

                IsTimeReady(task);

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }
        private static bool IsTimeReady(Task task)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - task.Period.StartDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (!task.IsRunning && delta < 10)
            {
                StartTask(task);
                return true;
            }

            return false;
        }

        private static bool IsProcessRunning(Task task)
        {
            Process[] processes = Process.GetProcessesByName(task.ExecutablePath);

            return processes.Length != 0;
        }
    }
}
