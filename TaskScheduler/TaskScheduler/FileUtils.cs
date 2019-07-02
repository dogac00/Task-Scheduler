using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class FileUtils
    {
        public static void CheckIfFileExists(string path)
        {
            if (!File.Exists(path))
            {
                CreateNewFile(path);
            }
        }

        public static void CreateNewFile(string path)
        {
            File.Create(path).Close();
            File.WriteAllText(path, "{}");
        }
    }
}
