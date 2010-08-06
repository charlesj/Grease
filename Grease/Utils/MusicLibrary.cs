using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grease.Utils
{
    class MusicLibrary
    {
        public List<Mp3Info> Songs { get; set; }

        public Mp3Info GetRandomMp3()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            return Songs[rand.Next(Songs.Count - 1)];
        }
    }
}
