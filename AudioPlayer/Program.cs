using AudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Media;

namespace Audioplayer
{
    [Flags] 
    public enum Genres 
    { 
        Pop = 1, Rock = 2, Metal = 4, Electro = 8  
    }
    public enum Videoformate
    {
        HD =1, MPEG =2
    }
    class Program
    {

        
        static void Main(string[] args)
        {
            ColorSkin ColorSkn = new ColorSkin(ConsoleColor.DarkYellow); 
            ClassicSkin ClassicSkn = new ClassicSkin();  
            Player player = new Player(ColorSkn); 
            player.Items = new  List<Song>(); 
            
          
            player.Clear();

            Console.WriteLine("Clear2    " + player.Items.Count); // show 0//
            player.Load(@"D:\ДЗ\playerload\audio\wav");
            Console.WriteLine("load wav" + player.Items.Count); // show 1//
            foreach (var item in player.Items)
            {
                Console.WriteLine(item.Path);
            }
            player.Play();

            WriteLine("Now we try to use your keyboard");

            
            while (true)
            {
                switch (ReadLine())
                {
                    case "a":
                        {
                            player.VolumeUp();
                            break;
                        }
                    case "s":
                        {
                            player.VolumeDown();
                            break;
                        }
                    case "d":
                        {
                            player.Play();
                            break;
                        }
                    case "q":
                        {
                            player.Lock();
                            break;
                        }
                    case "w":
                        {
                            player.UnLock();
                            break;
                        }
                    case "e":
                        {
                            player.Start();
                            break;
                        }
                    case "r":
                        {
                            player.Stop();
                            break;
                        }

                }
            }
            player.Dispose();
            ReadLine();
        }
    }
}