using AudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public static class StringExtension
    {
        public static string StringSeparator(this string str)
        {
            string sInsert = str.Insert(13, "^Nnn");
            string[] separ = { "^Nnn" };
            string[] sss = sInsert.Split(separ, StringSplitOptions.None);
            string lastvar = sss[0] + "...";
            return lastvar;
        }
    }
}
