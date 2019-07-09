using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskScheduler
{
    static class TimerUtils
    {
        private static readonly Form1 Form = Form1.Form;

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

        public static void DisposeTimer(Timer timer)
        {
            Form1.Form.Timers.RemoveAll(t => t.Timer == timer);

            timer.DisposeCompletely();
        }

        public static void DisposeTimers(Task task)
        {
            var timers = Form1.Form.Timers.FindAll(t => t.TaskName == task.Name);

            DisposeTaskTimers(timers);

            Form1.Form.Timers.RemoveAll(t => t.TaskName == task.Name);
        }

        public static void DisposeTimers(int taskId)
        {
            var task = Form.Repository.GetTaskById(taskId);

            if (task == null) return;

            DisposeTimers(task);
        }

        private static void DisposeTaskTimers(List<TaskTimer> taskTimers)
        {
            foreach (var taskTimer in taskTimers)
                taskTimer.Timer.DisposeCompletely();
        }

        public static Timer CreateTimer(Action action, long dueTime, long period)
        {
            return new Timer((state) => action.Invoke(), null, dueTime, period);
        }

        public static Timer CreateTimer(Action action, TimeSpan dueTime, TimeSpan period)
        {
            return new Timer((state) => action.Invoke(), null, dueTime, period);
        }

        public static Timer CreateTimer(Delegate action, long dueTime, long period)
        {
            return new Timer((state) => action.DynamicInvoke(), null, dueTime, period);
        }

        public static Timer CreateTimer(Delegate action, TimeSpan dueTime, TimeSpan period)
        {
            return new Timer((state) => action.DynamicInvoke(), null, dueTime, period);
        }

        

        

        
    }
}
