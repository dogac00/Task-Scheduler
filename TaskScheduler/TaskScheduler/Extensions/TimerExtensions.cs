using NLog;
using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskScheduler
{
    static class TimerExtensions
    {
        private static volatile object _threadLock = new object();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void DisposeAllResources(this Timer timer)
        {
            // You must be careful if you're going to use this.
            // It blocks the UI thread and can freeze the application.

            timer.Change(Timeout.Infinite, Timeout.Infinite);

            lock (_threadLock)
            {
                if (timer != null)
                {
                    ManualResetEvent waitHandle = new ManualResetEvent(false);

                    if (timer.Dispose(waitHandle))
                    {
                        // Timer has not been disposed by someone else
                        waitHandle.WaitOne();
                    }

                    waitHandle.Dispose();   // Only close if Dispose has completed succesful

                    timer = null;
                }
            }
        }

        public static void StopAndDisposeTimer(this Timer timer)
        {
            try
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);

                timer.Dispose();
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
