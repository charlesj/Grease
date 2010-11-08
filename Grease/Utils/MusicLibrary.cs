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
        public int CurrentSongIndex = -1;
        private Random rand = new Random(Convert.ToInt32(DateTime.Now.Second + DateTime.Now.Year + DateTime.Now.DayOfYear));
        public int songcount = 0;

        public MusicLibrary()
        {
            Songs = new List<Mp3Info>();
            PlayedSongs = new List<Mp3Info>();
        }

        private Mp3Info GetSerialMp3()
        {
            Grease.Utils.Mp3Info test;
            if (PlayedSongs.Count == Songs.Count || PlayedSongs.Count == 0)
                test = Songs[0];
            else
            {
                if (PlayedSongs.Count - 1 == Songs.Count)
                    test = Songs[0];
                else
                    test = Songs[PlayedSongs.Count];
            }
            return test;
        }

        private Mp3Info GetRandomMp3()
        {
            var test = Songs[rand.Next(Songs.Count - 1)];
            //var numPlayedToCheck = 500;
            //var min = Math.Min(numPlayedToCheck, PlayedSongs.Count);
            //var hasPlayedRecently = true;
            //while (hasPlayedRecently)
            //{
            //    bool found = false;
            //    for (int i = PlayedSongs.Count - min; i < PlayedSongs.Count; i++)
            //    {
            //        if (test.FullPath == PlayedSongs[i].FullPath)
            //            found = true;
            //    }
            //    if (!found)
            //        hasPlayedRecently = false;
            //    else
            //        test = Songs[rand.Next(Songs.Count - 1)];
            //}
            return test;
        }

        public Mp3Info Next(bool isshuffle = false,bool isrepeat = false)
        {
            
            if (!isrepeat)
            {
                if (CurrentSongIndex < PlayedSongs.Count)
                {
                    if (CurrentSongIndex + 1 == PlayedSongs.Count)
                    {
                        if (isshuffle)
                            PlayedSongs.Add(GetRandomMp3());
                        else
                            PlayedSongs.Add(GetSerialMp3());
                    }
                }
                //vrd
                songcount += 1;

                if (CurrentSongIndex == Songs.Count)
                    CurrentSongIndex = 0;
                else
                    CurrentSongIndex += 1;
            }
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
            //vrd
            songcount -= 1;
            //
            CurrentSongIndex -= 1;
            return PlayedSongs[CurrentSongIndex];
        }

    }
}