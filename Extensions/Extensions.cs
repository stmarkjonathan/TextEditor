using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace TextEditor.Extensions
{
    public static class Extensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] {' '},
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}
