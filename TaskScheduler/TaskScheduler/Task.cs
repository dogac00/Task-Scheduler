using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class Task
    {
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public bool IsRunning { get; set; }
        public int ProcessId { get; set; }
        public TaskPeriod Period { get; set; }
        public EmailInfo EmailInfo { get; set; }
    }

    class TaskPeriod
    {
        public StartProperty Property { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeBetween { get; set; }
    }

    public enum StartProperty
    {
        Once, Periodically, Consecutively,
    }

    public enum Interval
    {
        Week, Day, Hour, Min
    }
}
