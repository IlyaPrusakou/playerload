using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class ClassicSkin : Skin 
    {
        public ClassicSkin() : base()
        {

        }
        public override void Clear() 
        {
            Console.Clear(); 
        }

        public override void Render(string str) 
        {
            string s = str; 
            Console.WriteLine("><" + s + "><"); 
        }
    }
}
