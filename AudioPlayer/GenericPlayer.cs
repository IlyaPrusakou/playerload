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
    public class GenericPlayer<T>  where T: ItemPlaying // GenericPlayerHomework 
    {
        private int volume;
        private bool playing;
        public const int minVolume = 0;
        public const int maxVolume = 100;
        public bool IsLock { get; set; }
        public List<T> Items { get; set; } // GenericPlayerHomework
        public Random Rnd { get; set; } = new Random();
        public Skin SkinForm { get; set; }
        public GenericPlayer()
        {

        }

        public GenericPlayer(Skin skn)
        {
            SkinForm = skn;
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
        public void ParametrSong(params T[] itemList) // GenericPlayerHomework
        {
            foreach (T item in itemList)
            {
                SkinForm.Render(item.Title); 
            }
        }
        public (string Title, bool IsNext, (int Sec, int Min, int Hour)) GetItemData(T item)  // GenericPlayerHomework
        {
            var (str, boo, sec, min, hour) = item;
            string s = str;
            bool d = boo;
            int f = sec;
            int f1 = min;
            int f2 = hour;
            return (Title: s, IsNext: d, (Sec: f, Min: f1, Hour: f2));
        }
        public void ListItem(List<T> list) // GenericPlayerHomework
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

        public List<T> FilterByGenres(List<T> items, Genres genre)  // GenericPlayerHomework
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
        //private FileInfo[] GetWav(string directoryPath)
        //{
            //DirectoryInfo dir = new DirectoryInfo(directoryPath);
            //FileInfo[] files = dir.GetFiles();
            //return files;
        //}
        //private byte[] CreateByteFromWav(long lengtOfStream)
        //{
            //byte[] bytemass = new byte[lengtOfStream];
            //return bytemass;
        //}
        //public void Load(string directoryPath)
        //{
            //List<T> listOfLoadedSongs = new List<T>();
            //FileInfo[] files = GetWav(directoryPath);
            //foreach (FileInfo item in files)
            //{

                //T itemSong = new T();
                //try
                //{
                    //using (FileStream fs = new FileStream(item.FullName, FileMode.Open))
                    //{
                        //byte[] bytemass = CreateByteFromWav(fs.Length);
                        //int len = Convert.ToInt32(bytemass.Length);
                        //fs.Read(bytemass, 0, len);
                        //itemSong.ItemByteData = bytemass;
                    //}
                //}
                //catch (FileNotFoundException)
                //{
                   // Console.WriteLine("File has not found");
                //}
                //if (itemSong.ItemByteData.Length > 0) { listOfLoadedSongs.Add(itemSong); }
            //}
            //Items = listOfLoadedSongs;
        //}
        public void Clear()
        {
            List<T> emptyList = new List<T>();
            Items = emptyList;
        }
        private void SerializeClass(T item, string filepath, XmlSerializer xs, XmlWriterSettings set)
        {
                using (FileStream fs = File.Create(filepath))
                {
                     using (XmlWriter str =  XmlWriter.Create(fs, set))
                    {
                        xs.Serialize(str, item);
                    }
                }
        }
        
        public void Save(string directory)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            XmlWriterSettings set = new XmlWriterSettings();
            set.Indent = true;
            foreach (T item in Items)
            {
                string filepath = directory + @"\" + item.Title + ".xml";
                SerializeClass(item, filepath, xs, set);
            }
        }
        private T DeserializeClass(FileInfo file, XmlSerializer xs)
        {
            T song;
            using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
            {
                using (XmlReader rdr = XmlReader.Create(fs))
                {
                    song = (T)xs.Deserialize(rdr);
                }
            }
            return song;
        }
        public void LoadPlayList(string directoryPath)
        {

            XmlSerializer xs = new XmlSerializer(typeof(T));
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            FileInfo[] files = dir.GetFiles("*.xml*"); // надо еще проверить
            List<T> listOfLoadedSongs = new List<T>();
            T song=null;
            foreach (FileInfo item in files)
            {
                try
                {
                    
                         song = DeserializeClass(item, xs);
                    
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File has not found");
                }
                 listOfLoadedSongs.Add(song);
            }
            Items = listOfLoadedSongs;
        }
    }
}
