using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    class ProcessUtils
    {
        public static bool IsProcessRunning(Task task)
        {
            if (task == null) return false;

            return IsProcessRunning(task.ProcessId);
        }

        public static bool IsProcessRunning(int processId)
        {
            return Process.GetProcesses().Any(x => x.Id == processId);
        }
    }
}
