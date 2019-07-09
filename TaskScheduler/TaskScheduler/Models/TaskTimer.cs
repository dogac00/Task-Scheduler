using System;
using System.Threading;

namespace TaskScheduler
{
    public class TaskTimer
    {
        public Timer Timer { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan PeriodTime { get; set; }
    }
}
