using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using AudioPlayer;
using System.Media;

namespace Audioplayer
{
    public class Player: GenericPlayer<Song>, IDisposable  //
    {
        private bool disposed = false;//
        private SoundPlayer soundplayer; //
        public Player()
        {
            soundplayer = new SoundPlayer(); 
        }

        public Player(Skin skn)
        {
            soundplayer = new SoundPlayer(); 
            SkinForm = skn;
        }
        public override void Play(bool Loop = false)//
        {
            
            if (Loop == false)//
            {
                ShufleExtension.ExtenShufle(Items);//
            }
            else//
            {
                for (int i = 0; i < 5; i++)//
                {
                    ShufleExtension.ExtenShufle(Items);//
                }
            }
            if (Playing == true)//
            {
                foreach (Song song in Items)//
                {
                    soundplayer.SoundLocation = song.Path;//
                    soundplayer.Load();//
                    soundplayer.PlaySync();//
                }
            }
        }

        private FileInfo[] GetWav(string directoryPath, string pattern = null) 
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath); 
            FileInfo[] files = null;
            if (pattern == null) { files = dir.GetFiles(); }
            else if (pattern != null) { files = dir.GetFiles(pattern); }
            return files; //AL6-Player1/2-AudioFiles.//
        }
         public override void Load(string directoryPath, string pattern = null) //AL6-Player1/2-AudioFiles.//
        {
            List<Song> listOfLoadedSongs = new List<Song>(); //AL6-Player1/2-AudioFiles.//
            FileInfo[] files = GetWav(directoryPath); //AL6-Player1/2-AudioFiles.//
            foreach (FileInfo item in files) //AL6-Player1/2-AudioFiles.//
            {
                Song itemSong = new Song();  //AL6-Player1/2-AudioFiles.//
                itemSong.Path = item.FullName;
                listOfLoadedSongs.Add(itemSong); 
            }
            Items = listOfLoadedSongs; 
        }
        public override void Clear()
        {
            List<Song> emptyList = new List<Song>(); 
            Items = emptyList; 
        }
        
        public override void SavePlaylist(string directory, string name) //AL6-Player2/2-PlaylistSrlz.//
        {
            if (Items.Count != 0)
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<Song>));
                XmlWriterSettings set = new XmlWriterSettings();
                set.Indent = true;
                string filepath = directory + @"\" + name + @".xml";
                using (FileStream fs = File.Create(filepath))
                {
                    using (XmlWriter str = XmlWriter.Create(fs, set))
                    {
                        xs.Serialize(str, Items);
                    }
                }
            }
            else if (Items.Count == 0) { Console.WriteLine("Playlist is empty"); }
        }
        public override void LoadPlayList(string filepath) //AL6-Player2/2-PlaylistSrlz.//
        {

            XmlSerializer xs = new XmlSerializer(typeof(List<Song>)); 
            List<Song> listOfLoadedSongs; 
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                using (XmlReader str = XmlReader.Create(fs))
                {
                    listOfLoadedSongs = (List<Song>)xs.Deserialize(str);
                }
            }
            Items = listOfLoadedSongs; 
        }
        public void LyricsOutput() 
        {
            foreach (Song item in Items) 
            {
                SkinForm.Render($"{item.Title} --- {item.Lyrics}"); 
            }
        }
        public void Dispose()//
        {
            DisposeAlgo(true);//
            GC.SuppressFinalize(this);//
        }
        protected virtual void DisposeAlgo(bool disposing)//
        {
            if (!disposed)//
            {
                if (disposing)//
                {
                    Items = null;//
                    soundplayer = null;//
                    Rnd = null;//
                    SkinForm = null;//
                }
                disposed = true;//
            }
        }
        ~Player()//
        {
            DisposeAlgo(false);//
        }
    }
}