using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class ColorSkin: Skin 
    {
        ConsoleColor CnsClr { get; set; } 
        public ColorSkin() : this (0) 
        {
            CnsClr = ConsoleColor.Magenta; 
        }
        public ColorSkin(ConsoleColor clr) : base() 
        {
            CnsClr = clr; 
        }
        public override void Clear() 
        {
            Console.Clear(); 
             
        }

        public override void Render(string str) 
        {
            string s = str; 
            Console.BackgroundColor = CnsClr; 
            
            Console.WriteLine(s); 
        }
    }
}
