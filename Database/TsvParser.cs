using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    static class TsvParser
    {
        static public string[] Parse(this string line)
        {
            return line.Split('\t');
        }
    }
}
