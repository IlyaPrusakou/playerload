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
    public abstract class GenericPlayer<T>  where T: ItemPlaying 
    {
        private int volume;
        private bool playing;
        public const int minVolume = 0;
        public const int maxVolume = 100;
        public bool IsLock { get; set; }
        public List<T> Items { get; set; } 
        public Random Rnd { get; set; } = new Random();
        public Skin SkinForm { get; set; }
        public (string Title, bool IsNext, (int Sec, int Min, int Hour)) Data { get; set; }
        // <Events>
        public event Action PlayerStartedEvent;
        public event Action PlayerStoppedEvent;
        public event Action ItemStartedEvent; 
        public event Action VolumeChangedEvent;
        public event Action PlayerLockedEvent;
        public event Action PlayerUnLockedEvent;
        //<Events\>
        public void PlayNowItem(object sender, EventArgs e)
        {
            ItemStartedEvent?.Invoke();
        }
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
        //public void ListItem(List<T> list) 
        //{
            //foreach (T item in list)
            //{

                //string paramertString = "";
                //string outputString = "";
                //var tuple = GetItemData(item);
                //if (item.Like == true) { Console.ForegroundColor = ConsoleColor.Green; }
                //else if (item.Like == false) { Console.ForegroundColor = ConsoleColor.Red; }
                //else if (item.Like == null) { Console.ForegroundColor = ConsoleColor.Gray; }
                //paramertString = $"{tuple.Title}, {item.Genre} - {tuple.Item3.Hour}:{tuple.Item3.Min}:{tuple.Item3.Sec}";
                //if (Data.Title != null && Data.Title == item.Title)
                //{

                    //outputString = "!!!PLAY!!!" + paramertString.StringSeparator() + "!!!PLAY!!!";
                //}
                //else
                //{
                    //outputString = paramertString.StringSeparator();
                //}
                //SkinForm.Render(outputString);
                //Console.ResetColor();
            //}
        //}
        
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
            VolumeChangedEvent?.Invoke();
            Volume = Volume + 1;
           

        }
        public void VolumeDown()
        {
            VolumeChangedEvent?.Invoke();
            Volume = Volume - 1;
            
        }
        public void VolumeChange(int Step, string op)
        {
            VolumeChangedEvent?.Invoke();
            if (op == "+")
            {
                
                Volume = Volume + Step;
            }
            else if (op == "-")
            {
               
                Volume = Volume - Step;
            }
        }
        
        
        public bool Stop()
        {
            if (IsLock == false)
            {
                PlayerStoppedEvent?.Invoke();
     
                playing = false;
            }
            return playing;
        }
        public bool Start()
        {
            if (IsLock == false)
            {
                PlayerStartedEvent?.Invoke();
           
                playing = true;
            }
            return playing;
        }
        public void Pause()
        {
        }
        public void Lock()
        {
            PlayerLockedEvent?.Invoke();
       
            IsLock = true;
        }
        public void UnLock()
        {
            PlayerUnLockedEvent?.Invoke();
         
            IsLock = false;
        }
        public abstract void Play(bool Loop = false);
        
        public abstract void Load(string dirpath, string pattern = null);
        public abstract void Clear();

        public abstract void SavePlaylist(string directory, string name);

        public abstract void LoadPlayList(string filePath);
       
    }
}
