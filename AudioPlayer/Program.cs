using AudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Media;
using System.Collections.Concurrent;


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
        public static void RenderSongList( Player player) //-A.L7.Player2/2. Visualizer
        {
            foreach (Song item in player.Items)//-A.L7.Player2/2. Visualizer
            {

                string paramertString = "";//-A.L7.Player2/2. Visualizer
                string outputString = "";//-A.L7.Player2/2. Visualizer
                var tuple = player.GetItemData(item);//-A.L7.Player2/2. Visualizer
                if (item.Like == true) { Console.ForegroundColor = ConsoleColor.Green; }//-A.L7.Player2/2. Visualizer
                else if (item.Like == false) { Console.ForegroundColor = ConsoleColor.Red; }//-A.L7.Player2/2. Visualizer
                else if (item.Like == null) { Console.ForegroundColor = ConsoleColor.Gray; }//-A.L7.Player2/2. Visualizer
                paramertString = $"{tuple.Title}, {item.Genre} - {tuple.Item3.Hour}:{tuple.Item3.Min}:{tuple.Item3.Sec}";//-A.L7.Player2/2. Visualizer
                if (player.Data.Title != null && player.Data.Title == item.Title)//-A.L7.Player2/2. Visualizer
                {

                    outputString = "!!!PLAY!!!" + paramertString.StringSeparator() + "!!!PLAY!!!";//-A.L7.Player2/2. Visualizer
                }
                else
                {
                    outputString = paramertString.StringSeparator(); //-A.L7.Player2/2. Visualizer
                }
                player.SkinForm.Render(outputString); //-A.L7.Player2/2. Visualizer
                Console.ResetColor(); //-A.L7.Player2/2. Visualizer
            }
        }
        public static void Visualizer() //-A.L7.Player2/2. Visualizer
        {
            string StatusBar; //-A.L7.Player2/2. Visualizer
            string lockunlock = " "; //-A.L7.Player2/2. Visualizer
            string playing = " "; //-A.L7.Player2/2. Visualizer
            string title = " "; //-A.L7.Player2/2. Visualizer
            string commands = "press\n a/s - VolumeUp/VolumeDown,\n q/w - Lock/Unlock,\n e/r - Start/Stop,\n - d - PlaySong."; //-A.L7.Player2/2. Visualizer
            string volume = player.Volume.ToString(); //-A.L7.Player2/2. Visualizer
            if (player.IsLock == true) //-A.L7.Player2/2. Visualizer
            {
                lockunlock = "Player is locked"; //-A.L7.Player2/2. Visualizer
                title = ""; //-A.L7.Player2/2. Visualizer
            }
            else if (player.IsLock == false) { lockunlock = "Player is unlocked"; } //-A.L7.Player2/2. Visualizer
            if (player.Playing == true) { playing = "Player is playing"; } //-A.L7.Player2/2. Visualizer
            else if (player.Playing == false) { playing = "Player has stopped"; } //-A.L7.Player2/2. Visualizer
            title = player.Data.Title; //-A.L7.Player2/2. Visualizer
            StatusBar = lockunlock + " " + playing + " " + volume + "  " + title; //-A.L7.Player2/2. Visualizer
            player.SkinForm.Clear(); //-A.L7.Player2/2. Visualizer
            player.SkinForm.Render(StatusBar); //-A.L7.Player2/2. Visualizer
            RenderSongList(player); //-A.L7.Player2/2. Visualizer
            player.SkinForm.Render(commands); //-A.L7.Player2/2. Visualizer
            Console.ResetColor(); //-A.L7.Player2/2. Visualizer
        }

        
        static void Main(string[] args)
        {
            player.ItemListChangedEvent += Visualizer;
            player.ItemStartedEvent += Visualizer;
            player.PlayerLockedEvent += Visualizer;
            player.PlayerStartedEvent += Visualizer;
            player.PlayerStoppedEvent += Visualizer;
            player.PlayerUnLockedEvent += Visualizer;
            player.VolumeChangedEvent += Visualizer;
            player.ItemListChangedEvent += Visualizer;
            ColorSkin ColorSkn = new ColorSkin(ConsoleColor.DarkYellow); 
            ClassicSkin ClassicSkn = new ClassicSkin();
            player.SkinForm = ColorSkn;
            player.Items = new  List<Song>();
            ConsoleContex syncContext = new ConsoleContex(); // стал медленнее работать
            Visualizer();
            player.Load(@"D:\ДЗ\playerload\audio\wav");
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
                            player.PlayAsync();
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