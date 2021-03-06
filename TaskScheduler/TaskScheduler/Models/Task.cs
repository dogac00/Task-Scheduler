﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExecutablePath { get; set; }
        public bool IsRunning { get; set; }
        public int ProcessId { get; set; }
        public TaskPeriod Period { get; set; }
        public EmailInfo EmailInfo { get; set; }

        public override string ToString()
        {
            return $"Name : {Name}, Process Id : {ProcessId}";
        }
    }

    public class TaskPeriod
    {
        public StartProperty Property { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan TimeBetween { get; set; }

        public override string ToString()
        {
            return $"Property : {Property.ToString()}, " +
                $"Start Date : {StartDate}, Time Between : {TimeBetween}";
        }
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
