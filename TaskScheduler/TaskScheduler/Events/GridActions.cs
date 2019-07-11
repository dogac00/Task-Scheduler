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
            MainForm.Form.InvokeIfRequired(GridUtils.UpdateGrid);
        }
    }
}
