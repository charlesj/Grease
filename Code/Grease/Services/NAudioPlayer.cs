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
		/// The timer.
		/// </summary>
		private readonly DispatcherTimer timer;
	
		/// <summary>
		/// The _player.
		/// </summary>
		private IWavePlayer player;

		/// <summary>
		/// The file.
		/// </summary>
		private AudioFileReader file;

		/// <summary>
		/// The length value.
		/// </summary>
		private float lengthValue;

		/// <summary>
		/// The elapsed.
		/// </summary>
		private TimeSpan elapsed;

		/// <summary>
		/// The total time.
		/// </summary>
		private TimeSpan totalTime;

		/// <summary>
		/// The source.
		/// </summary>
		private string source;

		/// <summary>
		/// Initializes a new instance of the <see cref="NAudioPlayer"/> class.
		/// </summary>
		public NAudioPlayer()
		{
			this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 10) };
			this.timer.Tick += (sender, args) => this.Elapsed = this.file.CurrentTime;
			this.ObservableForProperty(model => model.Source).Subscribe(param => this.Stop());
			
			this.LengthValue = 0;
			this.TotalTime = new TimeSpan(0);
			this.Elapsed = new TimeSpan(0);
		}

		/// <summary>
		/// The track ended.
		/// </summary>
		public event TrackEndedEventHandler TrackEnded;

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
				this.RaiseAndSetIfChanged(ref this.source, value);
			}
		}

		/// <summary>
		/// Gets the length value.
		/// </summary>
		public float LengthValue
		{
			get
			{
				return this.lengthValue;
			}

			private set
			{
				this.RaiseAndSetIfChanged(ref this.lengthValue, value);
			}
		}

		/// <summary>
		/// Gets the elapsed.
		/// </summary>
		public TimeSpan Elapsed
		{
			get
			{
				return this.elapsed;
			}

			private set
			{
				this.RaiseAndSetIfChanged(ref this.elapsed, value);
			}
		}

		/// <summary>
		/// Gets the total time.
		/// </summary>
		public TimeSpan TotalTime
		{
			get
			{
				return this.totalTime;
			}

			private set
			{
				this.RaiseAndSetIfChanged(ref this.totalTime, value);
			}
		}

		/// <summary>
		/// The scrub.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
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
			if (this.player != null)
			{
				this.player.Pause();
			}

			this.timer.Stop();
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
				this.player.Init(this.file);
				this.player.PlaybackStopped += this.PlaybackStopped;
				this.player.Play();

				this.TotalTime = this.file.TotalTime;
			}
			else
			{
				this.player.Play();
			}

			this.timer.Start();
		}

		/// <summary>
		/// The raise track ended.
		/// </summary>
		protected virtual void RaiseTrackEnded()
		{
			var handler = this.TrackEnded;
			if (handler != null)
			{
				handler();
			}
		}

		/// <summary>
		/// The playback stopped.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void PlaybackStopped(object sender, StoppedEventArgs e)
		{
			this.RaiseTrackEnded();
			this.timer.Stop();
		}

		/// <summary>
		/// The stop.
		/// </summary>
		private void Stop()
		{
			this.timer.Stop();
			if (this.player != null)
			{
				this.player.Stop();
				this.player.Dispose();
				this.player = null;
			}
		}
	}
}