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
			get { return Library.Songs.Count; }
		}

		public MusicLibrary Library { get; private set; }

		public MusicFileEngine(IMusicPlayer player, IGreaseFileSystemAccess fsAccess, string path = null)
		{
			if (player == null) throw new ArgumentNullException("player");
			if (fsAccess == null) throw new ArgumentNullException("fsAccess");
			_player = player;
			_fsAccess = fsAccess;
			_isPlaying = false;
			Library = new MusicLibrary();
			if(!string.IsNullOrEmpty(path))
				Load(path);
			_player.ChangeVolume(1.0);
		}

		public void Play(bool next = false)
		{
			if (!_isPlaying || next)
			{
				//Pause();
				if (Library.Songs != null && Library.Songs.Count > 0)
				{
					if (_currSong == null || next)
					{
						_currSong = Library.GetNext();
						_player.Source = _currSong.FullPath;
					}
					_player.Play();
					_isPlaying = true;
				}
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
			if (Library.PlayedSongs.Count > 0)
			{
				Pause();
				_currSong = Library.GetPrevious();
				_player.Source =_currSong.FullPath;
				_player.Play();
				_isPlaying = true;
			}
		}

		public void ChangeVolume(double newVolume)
		{
			_player.ChangeVolume(newVolume);
		}

		public CurrentlyPlayingViewModel Current
		{
			get
			{
				if (Library.PlayedSongs.Count != 0)
				{
					return new CurrentlyPlayingViewModel
						{
							Album = _currSong.Album,
							Name = _currSong.Name,
							TrackNum = _currSong.TrackNum,
							HasImage = _currSong.HasImage,
							ImagePath = _currSong.ImagePath,
							Artist = _currSong.Artist
						};
				}
				return new CurrentlyPlayingViewModel
					{
						Album = string.Empty,
						Name = string.Empty,
						TrackNum = 0,
						HasImage = false,
						ImagePath = string.Empty,
						Artist = string.Empty
					};
			}
		}

		public void Load(string path)
		{
			Library.Songs = _fsAccess.GetMusicFiles(path);
		}
	}
}
