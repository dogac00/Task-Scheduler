using System;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler
{
    class EmailInfo
    {
        public bool IsToBeNotified { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public TimeSpan NoLongerThan { get; set; }
    }
}
