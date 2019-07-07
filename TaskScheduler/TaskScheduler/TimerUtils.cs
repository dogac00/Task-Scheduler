using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskScheduler
{
    class TimerUtils
    {
        private static volatile object _threadLock = new object();

        public static void DisposeTimer(Timer timer)
        {
            RemoveTimer(timer);
        }

        public static void AddTimer(Timer timer, string taskName, string description, 
                                        TimeSpan start, TimeSpan period)
        {
            TaskTimer _timer = new TaskTimer
            {
                Timer = timer,
                TaskName = taskName,
                Description = description,
                StartTime = DateTime.Now.Add(start),
                PeriodTime = period
            };

            Form1.Form.Timers.Add(_timer);
        }

        public static void AddTimer(Timer timer, string taskName, string description, 
                                        long start, long period)
        {
            TaskTimer _timer = new TaskTimer
            {
                Timer = timer,
                TaskName = taskName,
                Description = description,
                StartTime = DateTime.Now.AddMilliseconds(start),
                PeriodTime = TimeSpan.FromMilliseconds(period)
            };

            Form1.Form.Timers.Add(_timer);
        }

        private static void RemoveTimer(Timer timer)
        {
            Form1.Form.Timers.RemoveAll(t => t.Timer == timer);
            DisposeTimerCompletely(timer);
        }

        private static void DisposeTimerCompletely(Timer timer)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            lock (_threadLock)
            {
                if (timer != null)
                {
                    ManualResetEvent waitHandle = new ManualResetEvent(false);

                    if (timer.Dispose(waitHandle))
                    {
                        // Timer has not been disposed by someone else
                        waitHandle.WaitOne();
                    }

                    waitHandle.Dispose();   // Only close if Dispose has completed succesful

                    timer = null;
                }
            }
        }

        public static void RemoveTimers(Task task)
        {
            DisposeTimers(task);

            RemoveTimers(task.Name);
        }

        public static void RemoveTimers(int taskId)
        {
            var task = JsonUtils.GetTaskById(taskId);

            if (task == null) return;

            RemoveTimers(task);
        }

        private static void RemoveTimers(string taskName)
        {
            Form1.Form.Timers.RemoveAll(t => t.TaskName == taskName);
        }

        private static void DisposeTimers(Task task)
        {
            var timers = Form1.Form.Timers.FindAll(t => t.TaskName == task.Name);

            DisposeTimers(timers);
        }

        private static void DisposeTimers(List<TaskTimer> timers)
        {
            foreach (var timer in timers)
            {
                timer.Timer.Dispose();
            }
        }
    }
}
