using System;
using System.Collections.Generic;

namespace Grease.Utils
{
    class MusicLibrary
    {
        public List<Mp3Info> Songs { get; set; }
        public List<Mp3Info> PlayedSongs { get; set; }
        private int _currentSongIndex = -1;
        private readonly Random _rand = new Random(Convert.ToInt32(DateTime.Now.Second+DateTime.Now.Year+DateTime.Now.DayOfYear));


        public MusicLibrary()
        {
            Songs = new List<Mp3Info>();
            PlayedSongs = new List<Mp3Info>();
        }

        private Mp3Info GetRandomMp3()
        {
            var test = Songs[_rand.Next(Songs.Count - 1)];
            var numPlayedToCheck = 500;
            var min = Math.Min(numPlayedToCheck, PlayedSongs.Count);
            var hasPlayedRecently = true;
            while (hasPlayedRecently)
            {
                bool found = false;
                for (int i = PlayedSongs.Count - min; i < PlayedSongs.Count; i++)
                {
                    if (test.FullPath == PlayedSongs[i].FullPath)
                        found = true;
                }
                if (!found)
                    hasPlayedRecently = false;
                else
                    test = Songs[_rand.Next(Songs.Count - 1)];
            }
            return test;
        }

        
        public Mp3Info Next()
        {
            if (_currentSongIndex < PlayedSongs.Count)
            {
                if (_currentSongIndex + 1 == PlayedSongs.Count)
                {
                    PlayedSongs.Add(GetRandomMp3());
                }
            }
            _currentSongIndex += 1;
            return PlayedSongs[_currentSongIndex];
        }

        public Mp3Info Previous()
        {
            if (_currentSongIndex < 0)
            {
                if (PlayedSongs.Count == 0)
                    PlayedSongs.Add(GetRandomMp3());
                _currentSongIndex = 0;
                return PlayedSongs[_currentSongIndex];
            }
            if (_currentSongIndex == 0)
                return PlayedSongs[_currentSongIndex];
            _currentSongIndex -= 1;
            return PlayedSongs[_currentSongIndex];
        }
        
    }
}
