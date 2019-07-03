using System;
using System.Windows.Forms;

namespace TaskScheduler
{
    public class GridTask
    {
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public bool IsRunning { get; set; }
        public int ProcessId { get; set; }
        public string StartProperty { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeBetween { get; set; }
        public string EmailAddress { get; set; }
    }
}
