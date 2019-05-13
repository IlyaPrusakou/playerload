using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AudioPlayer
{
    [Serializable]
    //[XmlRoot("ItemPlaying")]
    public abstract class ItemPlaying // GenericPlayerHomework
    {
        [XmlElement("Duration")]
        public int Duration { get; set; } // GenericPlayerHomework
        [XmlElement("Title")]
        public string Title { get; set; } // GenericPlayerHomework
        [XmlElement("Path")]
        public string Path { get; set; } // GenericPlayerHomework
        [XmlElement("Genre")]
        public Genres Genre { get; set; } // GenericPlayerHomework
        [XmlArray("Playlists")]
        [XmlArrayItem("Playlist")]
        public List<Playlist> playlists; // GenericPlayerHomework
        public bool IsNext { get; set; } // GenericPlayerHomework
        [XmlElement("Like")]
        public bool? Like { get; set; } // GenericPlayerHomework
        [XmlElement("ItemByteData")]
        public byte[] ItemByteData { get; set; }

        public ItemPlaying()
        {
            playlists = new List<Playlist>();
        }

        public abstract void LikeMethod(); // GenericPlayerHomework

        public abstract void DislikeMethod(); // GenericPlayerHomework

        public void Deconstruct(out string str, out bool boo, out int sec, out int min, out int hour) 
        {
            str = Title; 
            boo = IsNext; 
            sec = Duration; 
            min = sec / 60; 
            hour = sec / 3600; 
        }
    }
}
