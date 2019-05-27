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
        public static void Visualizer() 
        {
            string StatusBar; 
            string lockunlock = " ";
            string playing = " "; 
            string title = " "; 
            string commands = "press\n l - input and load songs\n a/s - VolumeUp/VolumeDown,\n q/w - Lock/Unlock,\n e/r - Start/Stop,\n - d - PlaySong."; 
            string volume = player.Volume.ToString(); 
            if (player.IsLock == true) 
            {
                lockunlock = "Player is locked"; 
            }
            else if (player.IsLock == false) { lockunlock = "Player is unlocked"; } 
            if (player.Playing == true) { playing = "Player is playing"; } 
            else if (player.Playing == false) { playing = "Player has stopped"; } 
            title = player.Data.Title;
            if (player.token.IsCancellationRequested) { title = "Canceled"; }
            StatusBar = lockunlock + " " + playing + " " + volume + "  " + title; 
            player.SkinForm.Clear();
            if (player.exceptionfield != null && player.token.IsCancellationRequested == false && player.Playing == true && player.IsLock == false)
            {
                if (player.exceptionfield.GetType() == typeof(PlayerException) || player.exceptionfield.GetType() == typeof(FailedToPlayException))
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(player.exceptionfield.Message);
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine(player.exceptionfield.Message);
                    Console.ResetColor();
                }
            }
            player.SkinForm.Render(StatusBar); 
            RenderSongList(player); 
            player.SkinForm.Render(commands); 
            Console.ResetColor(); 
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
            player.OnError += Visualizer;
            player.OnWarning += Visualizer;

            ColorSkin ColorSkn = new ColorSkin(ConsoleColor.DarkYellow); 
            ClassicSkin ClassicSkn = new ClassicSkin();
            player.SkinForm = ColorSkn;
            player.Items = new  List<Song>();
            ConsoleContex syncContext = new ConsoleContex(); // стал медленнее работать
            Visualizer();
           
            //player.Load(@"D:\ДЗ\playerload\audio\wav");
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
                    case "l":
                        {
                            //string path = Console.ReadLine(); Закоментил чтобы не вводить вручную
                            //player.Load(path);
                            player.Load(@"D:\ДЗ\playerload\audio\wav");
                            break;
                        }
                        

                }
            }
            player.Dispose();
            ReadLine();
        }
    }
}