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
            System.Threading.Timer temp = null;

            foreach (var t in Form1.Form.Timers)
            {
                if (t == timer)
                    temp = t;
            }

            try
            {
                DisposeTimer(temp);
                Form1.Form.Timers.Remove(temp);

            } catch { }
        }
    }
}
