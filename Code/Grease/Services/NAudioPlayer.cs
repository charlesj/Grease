// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NAudioPlayer.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Services
{
	using System;
	using System.Windows.Threading;

	using Grease.Core;

	using NAudio.Wave;

	using ReactiveUI;

	/// <summary>
	/// The wpf player.
	/// </summary>
	public class NAudioPlayer : ReactiveObject, IMusicPlayer
	{
		/// <summary>
		/// The _player.
		/// </summary>
		private IWavePlayer player;

		private AudioFileReader file;

		private float lengthValue;

		private TimeSpan elapsed;

		private TimeSpan totalTime;

		private string source;

		private readonly PlaybackTimer playbackTimer;

		private DispatcherTimer timer;

		/// <summary>
		/// Initializes a new instance of the <see cref="NAudioPlayer"/> class.
		/// </summary>
		public NAudioPlayer()
		{
			this.timer = new DispatcherTimer();
			this.timer.Interval = new TimeSpan(0,0,0,0,10);
			this.timer.Tick += (sender, args) => this.Elapsed = this.file.CurrentTime;
			this.playbackTimer = new PlaybackTimer();
			this.playbackTimer.ObservableForProperty(model => model.Elapsed).Subscribe(param => this.Elapsed = param.Value);
			this.ObservableForProperty(model => model.Source).Subscribe(param => this.Stop());
			
			this.LengthValue = 0;
			this.TotalTime = new TimeSpan(0);
			this.Elapsed = new TimeSpan(0);
		}

		private void Stop()
		{
			timer.Stop();
			if (this.player != null)
			{
				this.player.Stop();
				this.player.Dispose();
				this.player = null;
			}
		}

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		public string Source
		{
			get
			{
				return this.source;
			}

			set
			{
				this.RaiseAndSetIfChanged(model => model.Source, ref this.source, value);
			}
		}

		public float LengthValue
		{
			get
			{
				return this.lengthValue;
			}
			private set
			{
				this.RaiseAndSetIfChanged(model => model.LengthValue, ref this.lengthValue, value);
			}
		}

		public TimeSpan Elapsed
		{
			get
			{
				return this.elapsed;
			}
			private set
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
			private set
			{
				this.RaiseAndSetIfChanged(model => model.TotalTime, ref this.totalTime, value);
			}
		}

		public void Scrub(double value)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The to change.
		/// </param>
		public void ChangeVolume(float newVolume)
		{
			if (this.file != null)
			{
				this.file.Volume = newVolume;
			}
		}

		/// <summary>
		/// The pause.
		/// </summary>
		public void Pause()
		{
			this.player.Pause();
			//this.playbackTimer.Pause();
			timer.Stop();
		}

		/// <summary>
		/// The play.
		/// </summary>
		public void Play()
		{
			if (this.player == null)
			{
				this.player = new WaveOut();
				this.file = new AudioFileReader(this.source) { Volume = 0.8f };
				this.player.Init(file);
				this.player.PlaybackStopped += this.PlaybackStopped;
				this.player.Play();
				//this.playbackTimer.Start();

				this.TotalTime = this.file.TotalTime;
			}
			else
			{
				this.player.Play();
			}
			this.timer.Start();
		}

		private void PlaybackStopped(object sender, StoppedEventArgs e)
		{
			this.RaiseTrackEnded();
			this.playbackTimer.Stop();
		}

		public event TrackEndedEventHandler TrackEnded;

		protected virtual void RaiseTrackEnded()
		{
			var handler = this.TrackEnded;
			if (handler != null)
			{
				handler();
			}
		}



		
	}
}