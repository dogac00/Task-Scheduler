using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskScheduler
{
    static class StringExtensions
    {
        public static string RemoveAllWhiteSpace(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
    }
}
