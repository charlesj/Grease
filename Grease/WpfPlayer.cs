// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WpfPlayer.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Input;
	using System.Windows.Media;
	using System.Windows.Media.Animation;

	using Grease.Core;

	/// <summary>
	/// The wpf player.
	/// </summary>
	public class WpfPlayer : IMusicPlayer
	{
		/// <summary>
		/// The _elapsed.
		/// </summary>
		private readonly Label elapsed;

		/// <summary>
		/// The _player.
		/// </summary>
		private readonly MediaElement player;

		/// <summary>
		/// The _remaining.
		/// </summary>
		private readonly Label remaining;

		/// <summary>
		/// The _slider.
		/// </summary>
		private readonly Slider slider;

		/// <summary>
		/// The _timeline.
		/// </summary>
		private readonly MediaTimeline timeline;

		// private readonly Slider _slider;
		// private readonly Label _elapsed;
		// private readonly Label _remaining;

		/// <summary>
		/// Initializes a new instance of the <see cref="WpfPlayer"/> class.
		/// </summary>
		/// <param name="player">
		/// The player.
		/// </param>
		/// <param name="slider">
		/// The slider.
		/// </param>
		/// <param name="elapsed">
		/// The elapsed.
		/// </param>
		/// <param name="remaining">
		/// The remaining.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if player is null
		/// </exception>
		public WpfPlayer(MediaElement player, Slider slider, Label elapsed, Label remaining)
		{
			if (player == null)
			{
				throw new ArgumentNullException("player");
			}

			this.player = player;
			this.slider = slider;
			this.elapsed = elapsed;
			this.remaining = remaining;
			this.timeline = new MediaTimeline();
			this.timeline.CurrentTimeInvalidated += this.TimelineCurrentTimeInvalidated;
			this.player.MediaOpened += this.PlayerOnMediaOpened;
			this.slider.MouseUp += this.SliderOnMouseUp;

			// _slider = slider;
			// _elapsed = elapsed;
			// _remaining = remaining;
			// _player.SourceUpdated += SourceUpdated;
		}

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		public string Source
		{
			get
			{
				return this.timeline.Source.ToString();
			}

			set
			{
				this.timeline.Source = new Uri(value);
				this.player.Clock = this.timeline.CreateClock();
			}
		}

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The to change.
		/// </param>
		public void ChangeVolume(double newVolume)
		{
			this.player.Volume = newVolume;
		}

		/// <summary>
		/// The pause.
		/// </summary>
		public void Pause()
		{
			if (this.player.Clock != null && this.player.Clock.Controller != null)
			{
				this.player.Clock.Controller.Pause();
			}
		}

		/// <summary>
		/// The play.
		/// </summary>
		public void Play()
		{
			if (this.player.Clock != null && this.player.Clock.Controller != null)
			{
				if (this.player.Clock.IsPaused)
				{
					this.player.Clock.Controller.Resume();
				}
				else
				{
					this.player.Clock.Controller.Begin();
				}
			}
		}

		/// <summary>
		/// The player on media opened.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="routedEventArgs">
		/// The routed event args.
		/// </param>
		private void PlayerOnMediaOpened(object sender, RoutedEventArgs routedEventArgs)
		{
			this.slider.Maximum = this.player.Clock.NaturalDuration.TimeSpan.TotalMilliseconds;
		}

		/// <summary>
		/// The slider on mouse up.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="mouseButtonEventArgs">
		/// The mouse button event args.
		/// </param>
		private void SliderOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			var clockController = this.player.Clock.Controller;
			if (clockController != null)
			{
				clockController.Seek(TimeSpan.FromSeconds(this.slider.Value), TimeSeekOrigin.BeginTime);
			}
		}

		/// <summary>
		/// The timeline current time invalidated.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void TimelineCurrentTimeInvalidated(object sender, EventArgs e)
		{
			this.slider.Value = this.player.Position.TotalMilliseconds;
			this.elapsed.Content = this.player.Position.ToString("mm':'ss");
			if (this.player.Clock.NaturalDuration != Duration.Automatic)
			{
				var diff = this.player.Clock.NaturalDuration.TimeSpan - this.player.Position;
				this.remaining.Content = diff.ToString("mm':'ss");
			}
		}

		////private void SourceUpdated(object sender, DataTransferEventArgs ee)
		////{
		////    var tl = new MediaTimeline(new Uri(@"c:\temp\!numa.wmv"));

		////    _player.Clock = tl.CreateClock(true) as MediaClock;

		////    _player.MediaOpened += (o, e) =>
		////    {
		////        _slider.Maximum = _player.NaturalDuration.TimeSpan.Seconds;
		////        _player.Clock.Controller.Pause();
		////    };

		////    _slider.ValueChanged += (o, e) =>
		////    {
		////        _player.Clock.Controller.Seek(TimeSpan.FromSeconds(_slider.Value), TimeSeekOrigin.BeginTime);
		////    };
		////}
	}
}