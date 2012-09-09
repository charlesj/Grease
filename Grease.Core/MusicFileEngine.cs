// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicFileEngine.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the MusicFileEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;

	/// <summary>
	/// The music file engine.
	/// </summary>
	public class MusicFileEngine : IMusicEngine
	{
		/// <summary>
		/// The player.
		/// </summary>
		private readonly IMusicPlayer player;

		/// <summary>
		/// The fs access.
		/// </summary>
		private readonly IGreaseFileSystemAccess fileSystemAccess;

		/// <summary>
		/// The is playing.
		/// </summary>
		private bool currentlyPlaying;

		/// <summary>
		/// The curr song.
		/// </summary>
		private MusicFileInfo currSong;

		/// <summary>
		/// Initializes a new instance of the <see cref="MusicFileEngine"/> class.
		/// </summary>
		/// <param name="player">
		/// The player.
		/// </param>
		/// <param name="fileSystemAccess">
		/// The file system access.
		/// </param>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if arguments are null.  Don't do that.  Except for Path. Path can be null.  I don't know why.
		/// </exception>
		public MusicFileEngine(IMusicPlayer player, IGreaseFileSystemAccess fileSystemAccess, string path = null)
		{
			if (player == null)
			{
				throw new ArgumentNullException("player");
			}

			if (fileSystemAccess == null)
			{
				throw new ArgumentNullException("fileSystemAccess");
			}

			this.player = player;
			this.fileSystemAccess = fileSystemAccess;
			this.currentlyPlaying = false;
			this.Library = new MusicLibrary();
			if (!string.IsNullOrEmpty(path))
			{
				this.Load(path);
			}

			this.player.ChangeVolume(1.0);
		}

		/// <summary>
		/// Gets or sets a value indicating whether is playing.
		/// </summary>
		public bool CurrentlyPlaying
		{
			get { return this.currentlyPlaying; }
			set { this.currentlyPlaying = value; }
		}

		/// <summary>
		/// Gets the found count.
		/// </summary>
		public int FoundCount
		{
			get { return this.Library.Songs.Count; }
		}

		/// <summary>
		/// Gets the library.
		/// </summary>
		public MusicLibrary Library { get; private set; }

		/// <summary>
		/// Gets the current.
		/// </summary>
		public CurrentlyPlayingViewModel Current
		{
			get
			{
				if (this.Library.PlayedSongs.Count != 0)
				{
					return new CurrentlyPlayingViewModel
					{
						Album = this.currSong.Album,
						Name = this.currSong.Name,
						TrackNum = this.currSong.TrackNum,
						HasImage = this.currSong.HasImage,
						ImagePath = this.currSong.ImagePath,
						Artist = this.currSong.Artist
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

		/// <summary>
		/// The play.
		/// </summary>
		/// <param name="next">
		/// The next.
		/// </param>
		public void Play(bool next = false)
		{
			if (!this.currentlyPlaying || next)
			{
				// Pause();
				if (this.Library.Songs != null && this.Library.Songs.Count > 0)
				{
					if (this.currSong == null || next)
					{
						this.currSong = this.Library.GetNext();
						this.player.Source = this.currSong.FullPath;
					}

					this.player.Play();
					this.currentlyPlaying = true;
				}
			}
		}

		/// <summary>
		/// The pause.
		/// </summary>
		public void Pause()
		{
			this.currentlyPlaying = false;
			this.player.Pause();
		}

		/// <summary>
		/// The next.
		/// </summary>
		public void Next()
		{
			this.Play(true);
		}

		/// <summary>
		/// The previous.
		/// </summary>
		public void Previous()
		{
			// only do something if there is actually a previous song.
			if (this.Library.PlayedSongs.Count > 0)
			{
				this.Pause();
				this.currSong = this.Library.GetPrevious();
				this.player.Source = this.currSong.FullPath;
				this.player.Play();
				this.currentlyPlaying = true;
			}
		}

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The new volume.
		/// </param>
		public void ChangeVolume(double newVolume)
		{
			this.player.ChangeVolume(newVolume);
		}

		/// <summary>
		/// The load.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		public void Load(string path)
		{
			this.Library.Songs = this.fileSystemAccess.GetMusicFiles(path);
		}
	}
}
