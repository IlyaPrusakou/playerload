using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class ClassicSkin : Skin //A.L2.Player1/1
    {
        public ClassicSkin() : base() //A.L2.Player1/1
        {

        }
        public override void Clear() //A.L2.Player1/1
        {
            Console.Clear(); //A.L2.Player1/1
        }

        public override void Render(string str) //A.L2.Player1/1
        {
            string s = str; //A.L2.Player1/1
            Console.ResetColor(); //A.L2.Player1/1
            Console.WriteLine("><" + s + "><"); //A.L2.Player1/1
        }
    }
}
