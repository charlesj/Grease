﻿// --------------------------------------------------------------------------------------------------------------------
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
	using System.ComponentModel;

	/// <summary>
	/// The music file engine.
	/// </summary>
	public class MusicFileEngine : IMusicEngine, INotifyPropertyChanged
	{
		/// <summary>
		/// The player.
		/// </summary>
		private readonly IMusicPlayer player;

		/// <summary>
		/// The is playing.
		/// </summary>
		private bool currentlyPlaying;

		/// <summary>
		/// The curr song.
		/// </summary>
		private ITrackInfo currSong;

		/// <summary>
		/// The elapsed.
		/// </summary>
		private TimeSpan elapsed;

		/// <summary>
		/// The total time.
		/// </summary>
		private TimeSpan totalTime;

		/// <summary>
		/// Initializes a new instance of the <see cref="MusicFileEngine"/> class.
		/// </summary>
		/// <param name="player">
		/// The player.
		/// </param>
		/// <param name="library">The music library</param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if arguments are null.  Don't do that.  Except for Path. Path can be null.  I don't know why.
		/// </exception>
		public MusicFileEngine(IMusicPlayer player, IMusicLibrary library)
		{
			this.Library = library;
			if (player == null)
			{
				throw new ArgumentNullException("player");
			}

			if (library == null)
			{
				throw new ArgumentNullException("library");
			}

			this.player = player;
			this.player.PropertyChanged += this.PlayerOnPropertyChanged;
			this.player.TrackEnded += this.PlayerOnTrackEnded;
			this.currentlyPlaying = false;
		}

		/// <summary>
		/// The property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets or sets the elapsed.
		/// </summary>
		public TimeSpan Elapsed
		{
			get
			{
				return this.elapsed;
			}

			set
			{
				if (this.elapsed != value)
				{
					this.elapsed = value;
					this.RaisePropertyChanged("Elapsed");
				}
			}
		}

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		public TimeSpan TotalTime
		{
			get
			{
				return this.totalTime;
			}

			set
			{
				if (this.totalTime != value)
				{
					this.totalTime = value;
					this.RaisePropertyChanged("TotalTime");
				}
			}
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
		public IMusicLibrary Library { get; private set; }

		/// <summary>
		/// Gets the current.
		/// </summary>
		public ITrackInfo Current
		{
			get
			{
				if (this.Library.PlayedSongs.Count != 0)
				{
					return this.currSong;
				}

				return null;
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
			if (this.Library.PlayedSongs.Count == 0 || next)
			{
				// Pause();
				if (this.Library.Songs != null && this.Library.Songs.Count > 0)
				{
					if (this.currSong == null || next)
					{
						this.currSong = this.Library.GetNext();
						this.player.Source = this.currSong.FullPath;
						this.RaisePropertyChanged("Current");  // TODO: fix this.
					}
				}
			}

			if (this.currSong != null)
			{
				this.player.Play();
				this.currentlyPlaying = true;
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
				this.RaisePropertyChanged("Current");
			}
		}

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The new volume.
		/// </param>
		public void ChangeVolume(float newVolume)
		{
			this.player.ChangeVolume(newVolume);
		}

		/// <summary>
		/// The raise property changed.
		/// </summary>
		/// <param name="propertyName">
		/// The property name.
		/// </param>
		protected virtual void RaisePropertyChanged(string propertyName)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// The player on track ended.
		/// </summary>
		private void PlayerOnTrackEnded()
		{
			this.Next();
		}

		/// <summary>
		/// The player on property changed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="propertyChangedEventArgs">
		/// The property changed event args.
		/// </param>
		private void PlayerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
		{
			if (propertyChangedEventArgs.PropertyName == "Elapsed")
			{
				this.Elapsed = this.player.Elapsed;
			}

			if (propertyChangedEventArgs.PropertyName == "TotalTime")
			{
				this.TotalTime = this.player.TotalTime;
			}
		}
	}
}
