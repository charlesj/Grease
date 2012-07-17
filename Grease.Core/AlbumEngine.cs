using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grease.Core
{
    public class AlbumEngine : IMusicEngine
    {
        private readonly IMusicPlayer _player;
        private readonly IGreaseFileSystemAccess _fsAccess;

        public AlbumEngine (IMusicPlayer player, IGreaseFileSystemAccess fsAccess, string path)
        {
            _player = player;
            _fsAccess = fsAccess;

            Load(path);
        }

        public void Play(bool next = false)
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Next()
        {
            throw new NotImplementedException();
        }

        public void Previous()
        {
            throw new NotImplementedException();
        }

        public void ChangeVolume(double newVolume)
        {
            throw new NotImplementedException();
        }

        public void Load(string path)
        {
            throw new NotImplementedException();
        }

        public CurrentlyPlayingViewModel Current
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsPlaying
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int FoundCount
        {
            get { throw new NotImplementedException(); }
        }
    }
}
