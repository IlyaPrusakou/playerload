using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace AudioPlayer
{
    public abstract class GenericPlayer<T>  where T: ItemPlaying // GenericPlayerHomework 
    {
        private int volume;
        private bool playing;
        public const int minVolume = 0;
        public const int maxVolume = 100;
        public bool IsLock { get; set; }
        public List<T> Items { get; set; } // GenericPlayerHomework
        public Random Rnd { get; set; } = new Random();
        public Skin SkinForm { get; set; }
        //public GenericPlayer()
        //{

        //}

        //public GenericPlayer(Skin skn)
        //{
            //SkinForm = skn;
        //}
        public bool Playing
        {
            get
            {
                return playing;
            }
        }

        public int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value < minVolume)
                {
                    volume = minVolume;
                }
                else if (value > maxVolume)
                {
                    volume = maxVolume;
                }
                else
                {
                    volume = value;
                }
            }

        }
        public void ParametrSong(params T[] itemList) 
        {
            foreach (T item in itemList)
            {
                SkinForm.Render(item.Title); 
            }
        }
        public (string Title, bool IsNext, (int Sec, int Min, int Hour)) GetItemData(T item)  
        {
            var (str, boo, sec, min, hour) = item;
            string s = str;
            bool d = boo;
            int f = sec;
            int f1 = min;
            int f2 = hour;
            return (Title: s, IsNext: d, (Sec: f, Min: f1, Hour: f2));
        }
        public void ListItem(List<T> list) 
        {
            foreach (T item in list)
            {
                var tuple = GetItemData(item);
                if (item.Like == true) { Console.ForegroundColor = ConsoleColor.Green; }
                else if (item.Like == false) { Console.ForegroundColor = ConsoleColor.Red; }
                else if (item.Like == null) { Console.ResetColor(); }
                string paramertString = $"{tuple.Title}, {item.Genre} - {tuple.Item3.Hour}:{tuple.Item3.Min}:{tuple.Item3.Sec}";
                string outputString = paramertString.StringSeparator();
                SkinForm.Render(outputString); 
            }
        }

        public List<T> FilterByGenres(List<T> items, Genres genre) 
        {
            List<T> FilterdList = new List<T>();
            IEnumerable<T> selectedItems = from t in items
                                                     where (t.Genre & genre) == genre
                                                     select t;
            foreach (T t in selectedItems)
            {
                FilterdList.Add(t);
            }
            return FilterdList;
        }
        public void VolumeUp()
        {
            Volume = Volume + 1;
            SkinForm.Render($"Volume up {Volume}"); 

        }
        public void VolumeDown()
        {
            Volume = Volume - 1;
            SkinForm.Render("Volume " + Volume); 
        }
        public void VolumeChange(int Step, string op)
        {
            if (op == "+")
            {
                SkinForm.Render($"up volume {Step}"); 
                Volume = Volume + Step;
            }
            else if (op == "-")
            {
                SkinForm.Render($"down volume {Step}"); 
                Volume = Volume - Step;
            }
        }
        
        
        public bool Stop()
        {
            if (IsLock == false)
            {
                SkinForm.Render("Stop"); 
                playing = false;
            }
            return playing;
        }
        public bool Start()
        {
            if (IsLock == false)
            {
                SkinForm.Render("Start"); 
                playing = true;
            }
            return playing;
        }
        public void Pause()
        {
        }
        public void Lock()
        {
            SkinForm.Render("Player is locked"); 
            IsLock = true;
        }
        public void UnLock()
        {
            SkinForm.Render("Player is unlocked"); 
            IsLock = false;
        }
        public virtual void Play(bool Loop = false)
        {
            if (Loop == false)
            {
                ShufleExtension.ExtenShufle(Items);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    ShufleExtension.ExtenShufle(Items); 
                }
            }
            if (playing == true)
            {
                SkinForm.Render("to Play has started");
                for (int i = 0; i < Items.Count; i++)
                {
                    SkinForm.Render(Items[i].Title);
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }
        public abstract void Load(string dirpath, string pattern = null);
        public abstract void Clear();

        public abstract void SavePlaylist(string directory);

        public abstract void LoadPlayList(string directoryPath, string pattern = null);
       
    }
}
