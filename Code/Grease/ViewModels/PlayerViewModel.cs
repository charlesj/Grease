﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the PlayerViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;

	using Grease.Core;

	using ReactiveUI;
	using ReactiveUI.Routing;
	using ReactiveUI.Xaml;

	/// <summary>
	/// The player view model.
	/// </summary>
	public class PlayerViewModel : ReactiveObject, IPlayerViewModel
	{
		/// <summary>
		/// The elapsed.
		/// </summary>
		private TimeSpan elapsed;

		/// <summary>
		/// The total time.
		/// </summary>
		private TimeSpan totalTime;

		/// <summary>
		/// The volume.
		/// </summary>
		private float volume;

		/// <summary>
		/// The current song name.
		/// </summary>
		private string currentSongName;

		/// <summary>
		/// The current artist name.
		/// </summary>
		private string currentArtistName;

		/// <summary>
		/// The current album name.
		/// </summary>
		private string currentAlbumName;

		/// <summary>
		/// The status text.
		/// </summary>
		private string statusText;

		/// <summary>
		/// The timeline location.
		/// </summary>
		private double timelineLocation;

		/// <summary>
		/// The engine.
		/// </summary>
		private IMusicEngine engine;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerViewModel"/> class.
		/// </summary>
		/// <param name="hostScreen">
		/// The host screen.
		/// </param>
		public PlayerViewModel(IScreen hostScreen)
		{
			this.engine = RxApp.GetService<IMusicEngine>();
			this.HostScreen = hostScreen;
			
			this.PauseCommand = new ReactiveCommand();
			this.PauseCommand.Subscribe(param => this.engine.Pause());
			this.PlayCommand = new ReactiveCommand();
			this.PlayCommand.Subscribe(param => this.engine.Play(false));
			this.NextSongCommand = new ReactiveCommand();
			this.NextSongCommand.Subscribe(param => this.engine.Next());
			this.PreviousSongCommand = new ReactiveCommand();
			this.PreviousSongCommand.Subscribe(param => this.engine.Previous());
			this.ChangeSongLocationCommand = new ReactiveCommand();
			this.SongEndedCommand = new ReactiveCommand();
			this.LoadSongsCommand = new ReactiveCommand();
			this.SongOpenedCommand = new ReactiveCommand();

			// setup some default values
			this.Volume = .8f;
			this.CurrentSongName = this.CurrentArtistName = this.CurrentAlbumName = "No Song Playing";
			
			// setup interactions
			this.ObservableForProperty(model => model.Volume).Subscribe(param => this.engine.ChangeVolume(param.Value));
			this.engine.ObservableForProperty(model => model.Current).Subscribe(param => this.UpdateCurrentPlayingInfo(param.Value));
			this.engine.ObservableForProperty(model => model.Elapsed).Subscribe(param =>
			{
				this.Elapsed = param.Value;
				this.TimelineLocation = this.Elapsed.TotalMilliseconds / this.TotalTime.TotalMilliseconds;
			});

			this.engine.ObservableForProperty(model => model.TotalTime).Subscribe(param => this.TotalTime = param.Value);
		}

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
				this.RaiseAndSetIfChanged(model => model.Elapsed, ref this.elapsed, value);
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
				this.RaiseAndSetIfChanged(model => model.TotalTime, ref this.totalTime, value);
			}
		}

		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		public float Volume
		{
			get
			{
				return this.volume;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.Volume, ref this.volume, value);
			}
		}

		/// <summary>
		/// Gets or sets the current song name.
		/// </summary>
		public string CurrentSongName
		{
			get
			{
				return this.currentSongName;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.CurrentSongName, ref this.currentSongName, value);
			}
		}

		/// <summary>
		/// Gets or sets the current artist name.
		/// </summary>
		public string CurrentArtistName
		{
			get
			{
				return this.currentArtistName;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.CurrentArtistName, ref this.currentArtistName, value);
			}
		}

		/// <summary>
		/// Gets or sets the current album name.
		/// </summary>
		public string CurrentAlbumName
		{
			get
			{
				return this.currentAlbumName;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.CurrentAlbumName, ref this.currentAlbumName, value);
			}
		}

		/// <summary>
		/// Gets or sets the status text.
		/// </summary>
		public string StatusText
		{
			get
			{
				return this.statusText;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.StatusText, ref this.statusText, value);
			}
		}

		/// <summary>
		/// Gets or sets the timeline location.
		/// </summary>
		public double TimelineLocation
		{
			get
			{
				return this.timelineLocation;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.TimelineLocation, ref this.timelineLocation, value);
			}
		}

		/// <summary>
		/// Gets the play command.
		/// </summary>
		public ReactiveCommand PlayCommand { get; private set; }

		/// <summary>
		/// Gets the pause command.
		/// </summary>
		public ReactiveCommand PauseCommand { get; private set; }

		/// <summary>
		/// Gets the next song command.
		/// </summary>
		public ReactiveCommand NextSongCommand { get; private set; }

		/// <summary>
		/// Gets the previous song command.
		/// </summary>
		public ReactiveCommand PreviousSongCommand { get; private set; }

		/// <summary>
		/// Gets the change song location command.
		/// </summary>
		public ReactiveCommand ChangeSongLocationCommand { get; private set; }

		/// <summary>
		/// Gets the load songs command.
		/// </summary>
		public ReactiveCommand LoadSongsCommand { get; private set; }

		/// <summary>
		/// Gets the song ended command.
		/// </summary>
		public ReactiveCommand SongEndedCommand { get; private set; }

		/// <summary>
		/// Gets the song opened command.
		/// </summary>
		public ReactiveCommand SongOpenedCommand { get; private set; }

		/// <summary>
		/// Gets the url path segment.
		/// </summary>
		public string UrlPathSegment
		{
			get
			{
				return "Player";
			}
		}

		/// <summary>
		/// Gets the host screen.
		/// </summary>
		public IScreen HostScreen { get; private set; }

		/// <summary>
		/// The update current playing info.
		/// </summary>
		/// <param name="track">
		/// The track.
		/// </param>
		private void UpdateCurrentPlayingInfo(ITrackInfo track)
		{
			this.CurrentAlbumName = track.Album;
			this.CurrentArtistName = track.Artist;
			this.CurrentSongName = track.Name;
		}
	}
}