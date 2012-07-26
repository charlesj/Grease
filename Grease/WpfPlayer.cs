using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Grease.Core;

namespace Grease
{
	public class WpfPlayer : IMusicPlayer
	{
		private readonly MediaElement _player;
		//private readonly Slider _slider;
		//private readonly Label _elapsed;
		//private readonly Label _remaining;

		public WpfPlayer(MediaElement player) // , Slider slider, Label elapsed, Label remaining)
		{
			if (player == null) throw new ArgumentNullException("player");
			_player = player;
			//_slider = slider;
			//_elapsed = elapsed;
			//_remaining = remaining;
			//_player.SourceUpdated += SourceUpdated;
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
			_player.Play();
		}

		public void Pause()
		{
			_player.Pause();
		}

		public void ChangeVolume(double toChange)
		{
			_player.Volume = toChange;
		}

		public string Source
		{
			get { return _player.Source.ToString(); }
			set { _player.Source = new Uri(value);}
		}
	}
}
