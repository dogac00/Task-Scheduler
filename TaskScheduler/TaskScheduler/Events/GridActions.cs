using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class GridActions
    {
        public static void InvokeFormIfRequired()
        {
            try
            {
                MainForm.Form.InvokeIfRequired(GridUtils.UpdateGrid);
            }
            catch (ObjectDisposedException)
            {
                // Simply ignore it.
            }
        }
    }
}
