using System;

namespace Grease.Core
{
    public class MusicFileEngine : IMusicEngine
    {
        private readonly IMusicPlayer _player;
        private readonly IGreaseFileSystemAccess _fsAccess;
        private bool _isPlaying;
        private MusicFileInfo _currSong;

        public bool IsPlaying
        {
            get { return _isPlaying; }
            set { _isPlaying = value; }
        }

        public int FoundCount
        {
            get { return MusicLibrary.Songs.Count; }
        }

        public MusicLibrary MusicLibrary { get; private set; }

        public MusicFileEngine(IMusicPlayer player, IGreaseFileSystemAccess fsAccess, string path = null)
        {
            if (player == null) throw new ArgumentNullException("player");
            if (fsAccess == null) throw new ArgumentNullException("fsAccess");
            _player = player;
            _fsAccess = fsAccess;
            _isPlaying = false;
            MusicLibrary = new MusicLibrary();
            if(!string.IsNullOrEmpty(path))
                Load(path);
            _player.ChangeVolume(1.0);
        }

        public void Play(bool next = false)
        {
            Pause();
            if (MusicLibrary.Songs != null && MusicLibrary.Songs.Count > 0)
            {
                if (_currSong == null || next)
                {
                    _currSong = MusicLibrary.GetNext();
                    _player.Source = _currSong.FullPath;
                }
                _player.Play();
                _isPlaying = true;
            }
        }

        public void Pause()
        {
            _isPlaying = false;
            _player.Pause();
        }

        public void Next()
        {
            Play(true);
        }

        public void Previous()
        {
            if (MusicLibrary.PlayedSongs.Count > 0)
            {
                Pause();
                _currSong = MusicLibrary.GetPrevious();
                _player.Source =_currSong.FullPath;
                _player.Play();
                _isPlaying = true;
            }
        }

        public void ChangeVolume(double newVolume)
        {
            _player.ChangeVolume(newVolume);
        }

        public CurrentlyPlayingInfo Current
        {
            get
            {
                return new CurrentlyPlayingInfo
                           {Album = _currSong.Album, Name = _currSong.Name, TrackNum = _currSong.TrackNum};
            }
        }

        public void Load(string path)
        {
            MusicLibrary.Songs = _fsAccess.GetMusicFiles(path);
        }
    }
}
