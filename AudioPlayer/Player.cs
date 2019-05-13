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

        public Player(Skin skn) : base(skn) 
        {

        }
        private FileInfo[] GetWav(string directoryPath) //AL6-Player1/2-AudioFiles.//
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath); //AL6-Player1/2-AudioFiles.//
            FileInfo[] files = dir.GetFiles();  //AL6-Player1/2-AudioFiles.//
            return files; //AL6-Player1/2-AudioFiles.//
        }
        private byte[] CreateByteFromWav(long lengtOfStream) //AL6-Player1/2-AudioFiles.// 
        {
            byte[] bytemass = new byte[lengtOfStream];  //AL6-Player1/2-AudioFiles.//
            return bytemass; //AL6-Player1/2-AudioFiles.//
        }
        public void Load(string directoryPath) //AL6-Player1/2-AudioFiles.//
        {
            List<Song> listOfLoadedSongs = new List<Song>(); //AL6-Player1/2-AudioFiles.//
            FileInfo[] files = GetWav(directoryPath); //AL6-Player1/2-AudioFiles.//
            foreach (FileInfo item in files) //AL6-Player1/2-AudioFiles.//
            {

                Song itemSong = new Song();  //AL6-Player1/2-AudioFiles.//
                try
                {
                    using (FileStream fs = new FileStream(item.FullName, FileMode.Open)) //AL6-Player1/2-AudioFiles.
                    {
                        byte[] bytemass = CreateByteFromWav(fs.Length);  //AL6-Player1/2-AudioFiles.//
                        int len = Convert.ToInt32(bytemass.Length); //AL6-Player1/2-AudioFiles.//
                        fs.Read(bytemass, 0, len); //AL6-Player1/2-AudioFiles.//
                        itemSong.ItemByteData = bytemass; //AL6-Player1/2-AudioFiles.//
                    }
                }
                catch (FileNotFoundException) //AL6-Player1/2-AudioFiles.//
                {
                    Console.WriteLine("File has not found"); //AL6-Player1/2-AudioFiles.//
                }
                if (itemSong.ItemByteData.Length > 0) { listOfLoadedSongs.Add(itemSong); } //AL6-Player1/2-AudioFiles.
            }
            Items = listOfLoadedSongs; //AL6-Player1/2-AudioFiles.//
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