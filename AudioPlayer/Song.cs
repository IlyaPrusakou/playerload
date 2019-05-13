using AudioPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audioplayer
{
    public class Song: ItemPlaying // GenericPlayerHomework
    {
        public string Lyrics { get; set; } // GenericPlayerHomework
        public Album Album { get; set; } // GenericPlayerHomework
        public Artist Artist { get; set; } // GenericPlayerHomework
        public GenericPlayer<Song> Player { get; set; } // GenericPlayerHomework

        public Song()
        {

        }

        public override void LikeMethod() // GenericPlayerHomework
        {
            base.Like = true; // GenericPlayerHomework
        }
        public override void DislikeMethod() // GenericPlayerHomework
        {
            base.Like = false; // GenericPlayerHomework
        }
    }
}