using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class CompareHelper : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (Convert.ToInt32(x[0]) < Convert.ToInt32(y[0]))
            {
                return -1;
            }
            else if (Convert.ToInt32(x[0]) > Convert.ToInt32(y[0]))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
