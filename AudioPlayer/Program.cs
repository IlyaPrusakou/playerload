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
        public static Player player = new Player();
        public static void RenderSongList( Player player)
        {
            foreach (Song item in player.Items)
            {

                string paramertString = "";
                string outputString = "";
                var tuple = player.GetItemData(item);
                if (item.Like == true) { Console.ForegroundColor = ConsoleColor.Green; }
                else if (item.Like == false) { Console.ForegroundColor = ConsoleColor.Red; }
                else if (item.Like == null) { Console.ForegroundColor = ConsoleColor.Gray; }
                paramertString = $"{tuple.Title}, {item.Genre} - {tuple.Item3.Hour}:{tuple.Item3.Min}:{tuple.Item3.Sec}";
                if (player.Data.Title != null && player.Data.Title == item.Title)
                {

                    outputString = "!!!PLAY!!!" + paramertString.StringSeparator() + "!!!PLAY!!!";
                }
                else
                {
                    outputString = paramertString.StringSeparator();
                }
                player.SkinForm.Render(outputString);
                Console.ResetColor();
            }
        }
        public static void Rerender()
        {
            string StatusBar;
            string lockunlock = " ";
            string playing = " ";
            string title = " "; 
            string volume = player.Volume.ToString();
            if (player.IsLock == true) { lockunlock = "Player is locked"; }
            else if (player.IsLock == false) { lockunlock = "Player is unlocked"; }
            if (player.Playing == true) { playing = "Player is playing"; }
            else if (player.Playing == false) { playing = "Player has stopped"; }
            title = player.Data.Title;
            StatusBar = lockunlock + " " + playing + " " + volume + "  " + title;
            player.SkinForm.Clear();
            player.SkinForm.Render(StatusBar);
            RenderSongList(player);
        }

        
        static void Main(string[] args)
        {
            player.ItemListChangedEvent += Rerender;
            player.ItemStartedEvent += Rerender;
            player.PlayerLockedEvent += Rerender;
            player.PlayerStartedEvent += Rerender;
            player.PlayerStoppedEvent += Rerender;
            player.PlayerUnLockedEvent += Rerender;
            player.VolumeChangedEvent += Rerender;
            player.ItemListChangedEvent += Rerender;
            ColorSkin ColorSkn = new ColorSkin(ConsoleColor.DarkYellow); 
            ClassicSkin ClassicSkn = new ClassicSkin();
            //Player player = new Player(ColorSkn); 
            player.SkinForm = ColorSkn;
            player.Items = new  List<Song>(); 
            
          
            player.Clear();

            
            player.Load(@"D:\ДЗ\playerload\audio\wav");
            player.Items[2].Like = true;
            player.Items[3].Like = false;

            player.Play();
            player.VolumeUp();
            player.Play();
            player.Lock();
            player.Play();
            player.UnLock();

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