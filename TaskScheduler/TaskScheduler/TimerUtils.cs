using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class TimerUtils
    {
        public static void DisposeTimer(System.Threading.Timer timer)
        {
            Form1.Form.Timers.Remove(timer);
            timer.Dispose();
        }

        public static void FindAndDisposeTimer(System.Threading.Timer timer)
        {
            foreach (var t in Form1.Form.Timers)
            {
                if (t == timer)
                    DisposeTimer(timer);
            }
        }
    }
}
