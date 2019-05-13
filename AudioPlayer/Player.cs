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

namespace Audioplayer
{
    public class Player: GenericPlayer<Song>  
    {
        public Player()
        {

        }

        public Player(Skin skn)
        {
        SkinForm = skn;
        }
        private FileInfo[] GetWav(string directoryPath, string pattern = null) //AL6-Player1/2-AudioFiles.//
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath); //AL6-Player1/2-AudioFiles.//
            FileInfo[] files = null;
            if (pattern == null) { files = dir.GetFiles(); }
            else if (pattern != null) { files = dir.GetFiles(pattern); }
            return files; //AL6-Player1/2-AudioFiles.//
        }
         public override void Load(string directoryPath) //AL6-Player1/2-AudioFiles.//
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
        private void SerializeClass(Song item, string filepath, XmlSerializer xs, XmlWriterSettings set) //AL6-Player2/2-PlaylistSrlz.
        {
            using (FileStream fs = File.Create(filepath))//AL6-Player2/2-PlaylistSrlz.//
            {
                using (XmlWriter str = XmlWriter.Create(fs, set))//AL6-Player2/2-PlaylistSrlz.
                {
                    xs.Serialize(str, item);//AL6-Player2/2-PlaylistSrlz.//
                }
            }
        }
        public override void SavePlaylist(string directory) //AL6-Player2/2-PlaylistSrlz.//
        {
            XmlSerializer xs = new XmlSerializer(typeof(Song)); //AL6-Player2/2-PlaylistSrlz.//
            XmlWriterSettings set = new XmlWriterSettings(); //AL6-Player2/2-PlaylistSrlz.//
            set.Indent = true; //AL6-Player2/2-PlaylistSrlz.//
            foreach (Song item in Items) //AL6-Player2/2-PlaylistSrlz.//
            {
                string filepath = directory + @"\" + item.Title + ".xml"; //AL6-Player2/2-PlaylistSrlz.//
                SerializeClass(item, filepath, xs, set); //AL6-Player2/2-PlaylistSrlz.//
            }
        }
        private Song DeserializeClass(FileInfo file, XmlSerializer xs) //AL6-Player2/2-PlaylistSrlz.//
        {
            Song song; //AL6-Player2/2-PlaylistSrlz.
            using (FileStream fs = new FileStream(file.FullName, FileMode.Open)) //AL6-Player2/2-PlaylistSrlz.
            {
                using (XmlReader rdr = XmlReader.Create(fs)) //AL6-Player2/2-PlaylistSrlz.//
                {
                    song = (Song)xs.Deserialize(rdr); //AL6-Player2/2-PlaylistSrlz.//
                }
            }
            return song; //AL6-Player2/2-PlaylistSrlz.//
        }
        public override void LoadPlayList(string directoryPath, string pattern = null) //AL6-Player2/2-PlaylistSrlz.//
        {

            XmlSerializer xs = new XmlSerializer(typeof(Song)); //AL6-Player2/2-PlaylistSrlz.//
            FileInfo[] files = GetWav(directoryPath, pattern);
            List<Song> listOfLoadedSongs = new List<Song>(); //AL6-Player2/2-PlaylistSrlz.
            Song song = null; //AL6-Player2/2-PlaylistSrlz.//
            foreach (FileInfo item in files) //AL6-Player2/2-PlaylistSrlz.//
            {
                song = DeserializeClass(item, xs); //AL6-Player2/2-PlaylistSrlz.//
                listOfLoadedSongs.Add(song); //AL6-Player2/2-PlaylistSrlz.//
            }
            Items = listOfLoadedSongs; //AL6-Player2/2-PlaylistSrlz.//
        }
        public void LyricsOutput() 
        {
            foreach (Song item in Items) 
            {
                SkinForm.Render($"{item.Title} --- {item.Lyrics}"); 
            }
        }
    }
}