using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mem.Helpers
{
    public static class StringExtensions
    {
        public static string Ellipsis(this string s, int len)
        {
            if (s.Length > len)
                return s.Substring(0, len - 3) + "...";
            else
                return s;
        }

    }
}
