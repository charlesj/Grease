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
            var test = Songs[rand.Next(Songs.Count - 1)];
            var numPlayedToCheck = 40;
            var min = Math.Min(numPlayedToCheck, PlayedSongs.Count);
            var hasPlayedRecently = true;
            while (hasPlayedRecently)
            {
                bool found = false;
                for (int i = 0; i < min; i++)
                {
                    if (test.FullPath == PlayedSongs[i].FullPath)
                        found = true;
                }
                if (!found)
                    hasPlayedRecently = false;
                else
                    test = Songs[rand.Next(Songs.Count - 1)];
            }
            return test;
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
