using System;

namespace TaskScheduler
{
    class EmailInfo
    {
        public bool IsToBeNotified { get; set; }
        public string EmailAddress { get; set; }
        public TimeSpan NoLongerThan { get; set; }
    }
}
