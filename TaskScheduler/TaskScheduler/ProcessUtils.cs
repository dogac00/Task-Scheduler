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
            return IsProcessRunning(task.ProcessId);
        }

        public static bool IsProcessRunning(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);

                return !process.HasExited;
            }
            catch
            {
                return false;
            }
        }
    }
}
