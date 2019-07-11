using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TaskScheduler
{
    static class TimerUtils
    {
        private static readonly MainForm Form = MainForm.Form;

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

            MainForm.Form.Timers.Add(_timer);
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

            MainForm.Form.Timers.Add(_timer);
        }

        public static void DisposeTimer(Timer timer)
        {
            MainForm.Form.Timers.RemoveAll(t => t.Timer == timer);

            timer.DisposeTimer();
        }

        public static void DisposeTimers(Task task)
        {
            var timers = MainForm.Form.Timers.FindAll(t => t.TaskName == task.Name);

            DisposeTimers(timers.Select(t => t.Timer));

            MainForm.Form.Timers.RemoveAll(t => t.TaskName == task.Name);
        }

        public static void DisposeTimers(int taskId)
        {
            var task = Form.Repository.GetTaskById(taskId);

            if (task == null) return;

            DisposeTimers(task);
        }

        private static void DisposeTimers(IEnumerable<Timer> timers)
        {
            foreach (var timer in timers)
                timer.DisposeTimer();
        }

        public static Timer CreateTimer(Action action, long dueTime, long period)
        {
            return new Timer((state) => action.Invoke(), null, dueTime, period);
        }

        public static Timer CreateTimer(Action action, TimeSpan dueTime, TimeSpan period)
        {
            return new Timer((state) => action.Invoke(), null, dueTime, period);
        }
    }
}
