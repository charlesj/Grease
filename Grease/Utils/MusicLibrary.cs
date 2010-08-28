using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grease.Utils
{
    class MusicLibrary
    {
        public List<Mp3Info> Songs { get; set; }
        public List<Mp3Info> PlayedSongs { get; set; }
        private int CurrentSongIndex = -1;

        public MusicLibrary()
        {
            Songs = new List<Mp3Info>();
            PlayedSongs = new List<Mp3Info>();
        }

        private Mp3Info GetRandomMp3()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            return Songs[rand.Next(Songs.Count - 1)];
        }

        
        public Mp3Info Next()
        {
            if (CurrentSongIndex < PlayedSongs.Count)
            {
                if (CurrentSongIndex + 1 == PlayedSongs.Count)
                {
                    PlayedSongs.Add(GetRandomMp3());
                }
            }
            CurrentSongIndex += 1;
            return PlayedSongs[CurrentSongIndex];
        }

        public Mp3Info Previous()
        {
            if (CurrentSongIndex < 0)
            {
                if (PlayedSongs.Count == 0)
                    PlayedSongs.Add(GetRandomMp3());
                CurrentSongIndex = 0;
                return PlayedSongs[CurrentSongIndex];
            }
            if (CurrentSongIndex == 0)
                return PlayedSongs[CurrentSongIndex];
            CurrentSongIndex -= 1;
            return PlayedSongs[CurrentSongIndex];
        }
        
    }
}
