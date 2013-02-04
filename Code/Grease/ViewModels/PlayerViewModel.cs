namespace Grease.ViewModels
{
	using System;
	using System.IO;

	using Grease.Core;

	using ReactiveUI;
	using ReactiveUI.Routing;
	using ReactiveUI.Xaml;

	public class PlayerViewModel : ReactiveObject, IPlayerViewModel
	{
		private TimeSpan elapsed;

		private TimeSpan totalTime;

		private float volume;

		private string currentSongName;

		private string currentArtistName;

		private string currentAlbumName;

		private string statusText;

		private double timelineLocation;

		private IMusicEngine engine;

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
				this.TimelineLocation = (this.Elapsed.TotalMilliseconds / this.TotalTime.TotalMilliseconds);
			});
			this.engine.ObservableForProperty(model => model.TotalTime).Subscribe(param =>  this.TotalTime = param.Value);
		}

		private void UpdateCurrentPlayingInfo(ITrackInfo track)
		{
			this.CurrentAlbumName = track.Album;
			this.CurrentArtistName = track.Artist;
			this.CurrentSongName = track.Name;
		}

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

		public ReactiveCommand PlayCommand { get; private set; }

		public ReactiveCommand PauseCommand { get; private set; }

		public ReactiveCommand NextSongCommand { get; private set; }

		public ReactiveCommand PreviousSongCommand { get; private set; }

		public ReactiveCommand ChangeSongLocationCommand { get; private set; }

		public ReactiveCommand LoadSongsCommand { get; private set; }

		public ReactiveCommand SongEndedCommand { get; private set; }

		public ReactiveCommand SongOpenedCommand { get; private set; }

		public string UrlPathSegment { get { return "Player"; } }

		public IScreen HostScreen { get; private set; }


	}
}
