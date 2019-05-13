using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    public class Video: ItemPlaying // GenericPlayerHomework
    {
        public Videoformate Formate { get; set; } // GenericPlayerHomework
        public List<Artist> Artist { get; set; } // GenericPlayerHomework
        public GenericPlayer<Video> Player { get; set; } // GenericPlayerHomework

        public Video()
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
