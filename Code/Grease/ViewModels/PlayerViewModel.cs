// --------------------------------------------------------------------------------------------------------------------
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
	using System.Collections.Specialized;

	using Grease.Core;

	using ReactiveUI;

	/// <summary>
	/// The player view model.
	/// </summary>
	public class PlayerViewModel : BaseViewModel, IPlayerViewModel
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

		private string formattedElapsed;

		private string formattedTotalTime;

		private string formattedVolume;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerViewModel"/> class.
		/// </summary>
		/// <param name="hostScreen">
		/// The host screen.
		/// </param>
		/// <param name="engine">
		/// The engine.
		/// </param>
		public PlayerViewModel(IScreen hostScreen, IMusicEngine engine)
			: base(hostScreen)
		{
			this.engine = engine;
			
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
			this.ObservableForProperty(model => model.Volume).Subscribe(param =>
				{
					this.engine.ChangeVolume(param.Value);
					this.FormattedVolume = this.Volume.ToString("P");
				});
			this.engine.ObservableForProperty(model => model.Current).Subscribe(param => this.UpdateCurrentPlayingInfo(param.Value));
			this.engine.ObservableForProperty(model => model.Elapsed).Subscribe(param =>
			{
				this.Elapsed = param.Value;
				this.TimelineLocation = this.Elapsed.TotalMilliseconds / this.TotalTime.TotalMilliseconds;
				this.FormattedElapsed = this.Elapsed.ToString("hh':'mm':'ss");
			});

			this.engine.ObservableForProperty(model => model.TotalTime).Subscribe(param =>
				{ 
					this.TotalTime = param.Value;
					this.FormattedTotalTime = this.TotalTime.ToString("hh':'mm':'ss");
				});

			this.SongsOnCollectionChanged(null, null);

			this.engine.Library.Songs.CollectionChanged += this.SongsOnCollectionChanged;
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
				this.RaiseAndSetIfChanged(ref this.elapsed, value);
			}
		}

		/// <summary>
		/// Gets or sets the formatted elapsed.
		/// </summary>
		public string FormattedElapsed
		{
			get
			{
				return this.formattedElapsed;
			}

			set
			{
				this.RaiseAndSetIfChanged(ref this.formattedElapsed, value);
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
				this.RaiseAndSetIfChanged(ref this.totalTime, value);
			}
		}

		/// <summary>
		/// Gets or sets the formatted total time.
		/// </summary>
		public string FormattedTotalTime
		{
			get
			{
				return this.formattedTotalTime;
			}

			set
			{
				this.RaiseAndSetIfChanged(ref this.formattedTotalTime, value);
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
				this.RaiseAndSetIfChanged(ref this.volume, value);
			}
		}

		/// <summary>
		/// Gets or sets the formatted volume.
		/// </summary>
		public string FormattedVolume
		{
			get
			{
				return this.formattedVolume;
			}

			set
			{
				this.RaiseAndSetIfChanged(ref this.formattedVolume, value);
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
				this.RaiseAndSetIfChanged(ref this.currentSongName, value);
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
				this.RaiseAndSetIfChanged(ref this.currentArtistName, value);
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
				this.RaiseAndSetIfChanged(ref this.currentAlbumName, value);
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
				this.RaiseAndSetIfChanged(ref this.statusText, value);
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
				this.RaiseAndSetIfChanged(ref this.timelineLocation, value);
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
		public override string UrlPathSegment
		{
			get
			{
				return "Player";
			}
		}

		/// <summary>
		/// The songs on collection changed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		private void SongsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			string message = string.Format("{0} songs loaded", this.engine.Library.Songs.Count);
			this.WriteToStatusBar(message);
		}

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
