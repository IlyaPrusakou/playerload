using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audioplayer
{
    public class Artist
    {
        public string name;
        public string nickname;
        public string country;
        public Band band;
        public List<Song> songs;

        // B5-Player5/10. Constructors.
        public Artist()
        {

        }
        public Artist(string _name)
        {
            name = _name;
        }
    }
}