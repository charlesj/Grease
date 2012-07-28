using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Grease.Core;

namespace Grease
{
	public class WpfPlayer : IMusicPlayer
	{
		private readonly MediaElement _player;
		private readonly Slider _slider;
		private readonly Label _elapsed;
		private readonly Label _remaining;
		private readonly MediaTimeline _timeline;
		
		//private readonly Slider _slider;
		//private readonly Label _elapsed;
		//private readonly Label _remaining;

		public WpfPlayer(MediaElement player, Slider slider, Label elapsed, Label remaining)
		{
			if (player == null) throw new ArgumentNullException("player");
			_player = player;
			_slider = slider;
			_elapsed = elapsed;
			_remaining = remaining;
			_timeline = new MediaTimeline();
			_timeline.CurrentTimeInvalidated += TimelineCurrentTimeInvalidated;
			_player.MediaOpened += PlayerOnMediaOpened;
			_slider.MouseUp += SliderOnMouseUp;
			//_slider = slider;
			//_elapsed = elapsed;
			//_remaining = remaining;
			//_player.SourceUpdated += SourceUpdated;
			
		}

		private void SliderOnMouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			_player.Clock.Controller.Seek(TimeSpan.FromSeconds(_slider.Value), TimeSeekOrigin.BeginTime);
		}


		private void PlayerOnMediaOpened(object sender, RoutedEventArgs routedEventArgs)
		{
			_slider.Maximum = _player.Clock.NaturalDuration.TimeSpan.TotalMilliseconds;
		}

		void TimelineCurrentTimeInvalidated(object sender, EventArgs e)
		{
			_slider.Value = _player.Position.TotalMilliseconds;
			_elapsed.Content = _player.Position.ToString("mm':'ss");
			if (_player.Clock.NaturalDuration != Duration.Automatic)
			{
				var diff = _player.Clock.NaturalDuration.TimeSpan - _player.Position;
				_remaining.Content = diff.ToString("mm':'ss");
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

		public void Play()
		{
			if (_player.Clock != null && _player.Clock.Controller != null)
			{
				if( _player.Clock.IsPaused)
					_player.Clock.Controller.Resume();
				else
				{
					_player.Clock.Controller.Begin();	
				}
				
			}
		}

		public void Pause()
		{
			if (_player.Clock != null && _player.Clock.Controller != null) 
				_player.Clock.Controller.Pause();
		}

		public void ChangeVolume(double toChange)
		{
			_player.Volume = toChange;
		}

		public string Source
		{
			get { return _timeline.Source.ToString(); }
			set
			{
				_timeline.Source = new Uri(value);
				_player.Clock = _timeline.CreateClock();
			}
		}
	}
}
