using Audioplayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioPlayer
{
    class VideoPlayer: GenericPlayer<Video> // GenericPlayerHomework
    {
        public VideoPlayer()
        {

        }

        public VideoPlayer(Skin skn)
        {
            SkinForm = skn;
        }
        public override void Play(bool param = false)
        {

        }
        public override void Clear()
        {
            throw new NotImplementedException();
        }

        public override void Load(string dirpath, string pattern = null)
        {
            throw new NotImplementedException();
        }

        public override void LoadPlayList(string directoryPath)
        {
            throw new NotImplementedException();
        }

        public override void SavePlaylist(string directory, string name)
        {
            throw new NotImplementedException();
        }
    }
}
