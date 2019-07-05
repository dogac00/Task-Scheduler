using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskScheduler
{
    class EmailInfo
    {
        public EmailInfo(bool isToBeNotified, List<string> emailAddresses, TimeSpan noLongerthan)
        {
            this.IsToBeNotified = isToBeNotified;
            this.EmailAddresses = emailAddresses;
            this.NoLongerThan = noLongerthan;
        }

        public EmailInfo() { }

        public bool IsToBeNotified { get; set; }
        public List<string> EmailAddresses { get; set; }
        public TimeSpan NoLongerThan { get; set; }
    }
}
