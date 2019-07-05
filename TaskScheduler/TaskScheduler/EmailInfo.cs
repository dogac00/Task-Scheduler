using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler
{
    class EmailInfo
    {
        public bool IsToBeNotified { get; set; }
        public List<string> EmailAddress { get; set; }
        public TimeSpan NoLongerThan { get; set; }
    }
}
