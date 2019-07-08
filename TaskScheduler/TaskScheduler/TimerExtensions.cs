using System;
using System.Collections.Generic;
using System.Threading;

namespace TaskScheduler
{
    static class TimerExtensions
    {
        private static volatile object _threadLock = new object();

        public static void DisposeCompletely(this Timer timer)
        {
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
    }
}
