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

        public static void SetDelayForConsecutive(Task task)
        {
            var startTimeSpan = TimeSpan.Zero;
            var delayTimeSpan = TimeSpanUtils
                .GenerateTimeSpan((float) Form1.Form.StartConsecutivelyDelay.Value,
                                    Form1.Form.GetInterval());

            var isTimerCreated = false;

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (isTimerCreated)
                {
                    RunTaskConsecutively(task);
                    TimerUtils.DisposeTimer(timer);
                } else
                {
                    isTimerCreated = true;
                }

            }, null, startTimeSpan, delayTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        public static Task CreateTask()
        {
            Task task = new Task
            {
                Name = Form1.Form.TaskName.Text,
                ExecutablePath = Form1.Form.TaskExecutablePath.Text,
                IsRunning = false,
                ProcessId = -1,
                Period = TaskPeriodUtils.SetPeriod(),
                EmailInfo = EmailUtils.SetEmailInfo()
            };

            return task;
        }

        public static void StartTaskForPeriodical(Task task)
        {
            RunTask(task);
            UpdateStatusEverySeconds(task, 5);
            SetTaskTimerForPeriods(task);
        }

        public static void RunTaskConsecutively(Task task)
        {
            RunTask(task);
            UpdateStatusConsecutively(task, 3);
        }

        public static bool RunTask(Task task)
        {
            try
            {
                task.ProcessId = Process.Start(task.ExecutablePath).Id;
                task.IsRunning = true;
                JsonUtils.UpdateTask(task, true, task.ProcessId);
                return true;
            }
            catch {
                MessageBox.Show("Invalid executable path.");
                JsonUtils.DeleteTask(task);
                return false;
            }
        }

        public static void UpdateStatusEverySeconds(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.FromSeconds(5);
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer((e) =>
            {

                if (!CheckIfTaskIsRunningAndUpdate(task))
                {
                    if (task.Period.Property == StartProperty.Once)
                        TimerUtils.FindAndDisposeTimer(timer);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        

        public static void UpdateStatusConsecutively(Task task, int everySeconds)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(everySeconds);

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((e) =>
            {

                if (!CheckIfTaskIsRunningAndUpdate(task))
                {
                    TimerUtils.DisposeTimer(timer);
                    SetDelayForConsecutive(task);
                }

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        private static bool CheckIfTaskIsRunningAndUpdate(Task taskArg)
        {
            Task task = JsonUtils.GetTask(taskArg);

            if (!ProcessUtils.IsProcessRunning(task))
            {
                if (task.IsRunning == true)
                {
                    task.IsRunning = false;
                    task.ProcessId = -1;
                    JsonUtils.UpdateTask(task, false, -1);
                }

                return false;
            }
            else
            {
                if (task.IsRunning == false)
                {
                    task.IsRunning = true;
                    JsonUtils.UpdateTask(task, true, task.ProcessId);
                }

                return true;
            }
        }

        public static void SetTaskStartingTimer(Task task, int runEverySeconds)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(runEverySeconds);

            System.Threading.Timer timer = null;
            timer = new System.Threading.Timer((e) =>
            {

                if (IsTaskTimeReady(task)) TimerUtils.DisposeTimer(timer);

            }, null, startTimeSpan, periodTimeSpan);

            Form1.Form.Timers.Add(timer);
        }

        private static bool IsTaskTimeReady(Task task)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - task.Period.StartDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (!task.IsRunning && delta < 10)
            {
                StartTaskForPeriodical(task);
                return true;
            }

            return false;
        }
    }
}
