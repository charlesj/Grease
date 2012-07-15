﻿using System;
using System.Collections.Generic;

namespace Grease.Core
{
    public class MusicLibrary
    {
        public List<MusicFileInfo> Songs { get; set; }
        public List<MusicFileInfo> PlayedSongs { get; set; }
        private int _currentSongIndex = -1;
        private readonly Random _rand = new Random(DateTime.Now.Millisecond);
        public int NumberToPlayBeforeRepeats { get; set; }

        public MusicLibrary(int numberToPlayBeforeRepeats = 500)
        {
            NumberToPlayBeforeRepeats = numberToPlayBeforeRepeats;
            Songs = new List<MusicFileInfo>();
            PlayedSongs = new List<MusicFileInfo>();
        }

        private MusicFileInfo GetRandomMusicFile()
        {
            var test = Songs[_rand.Next(Songs.Count - 1)];
            var min = Math.Min(NumberToPlayBeforeRepeats, PlayedSongs.Count);
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

        
        public MusicFileInfo GetNext()
        {
            if (_currentSongIndex < PlayedSongs.Count)
            {
                if (_currentSongIndex + 1 == PlayedSongs.Count)
                {
                    PlayedSongs.Add(GetRandomMusicFile());
                }
            }
            _currentSongIndex += 1;
            return PlayedSongs[_currentSongIndex];
        }

        public MusicFileInfo GetPrevious()
        {
            if (_currentSongIndex < 0)
            {
                if (PlayedSongs.Count == 0)
                    PlayedSongs.Add(GetRandomMusicFile());
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
