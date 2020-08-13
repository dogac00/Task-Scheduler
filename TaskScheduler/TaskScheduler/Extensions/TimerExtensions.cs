using NLog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskScheduler
{
    static class TimerExtensions
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        public static void StopAndDisposeTimer(this Timer timer)
        {
            try
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);

                timer.Dispose();
                
                timer = null; 
                // Not mandatory
                // To be garbage collected
            }
            catch (Exception e)
            {
                logger.Warn(e.Message);

                // We ignore it because the timer is already disposed or deleted

                // It can throw object disposed exception
                // Since we cannot prevent a few callbacks after disposal
            }
        }
    }
}
