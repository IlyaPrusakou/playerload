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
    public class Player: GenericPlayer<Song>  // GenericPlayerHomework
    {

        public Player(Skin skn) : base(skn) // GenericPlayerHomework
        {

        }
        private FileInfo[] GetWav(string directoryPath)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            FileInfo[] files = dir.GetFiles(); 
            return files;
        }
        private byte[] CreateByteFromWav(long lengtOfStream) 
        {
            byte[] bytemass = new byte[lengtOfStream]; 
            return bytemass;
        }
        public void Load(string directoryPath)
        {
            List<Song> listOfLoadedSongs = new List<Song>();
            FileInfo[] files = GetWav(directoryPath);
            foreach (FileInfo item in files)
            {

                Song itemSong = new Song(); 
                try
                {
                    using (FileStream fs = new FileStream(item.FullName, FileMode.Open))
                    {
                        byte[] bytemass = CreateByteFromWav(fs.Length); 
                        int len = Convert.ToInt32(bytemass.Length);
                        fs.Read(bytemass, 0, len);
                        itemSong.ItemByteData = bytemass;
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File has not found");
                }
                if (itemSong.ItemByteData.Length > 0) { listOfLoadedSongs.Add(itemSong); }
            }
            Items = listOfLoadedSongs;
        }

        public void LyricsOutput() // GenericPlayerHomework
        {
            foreach (Song item in Items) // GenericPlayerHomework
            {
                SkinForm.Render($"{item.Title} --- {item.Lyrics}"); // GenericPlayerHomework
            }
        }
    }
}