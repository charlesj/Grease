using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Grease.Core;

namespace Grease
{
    public class WpfPlayer : IMusicPlayer
    {
        private readonly MediaElement _player;

        public WpfPlayer(MediaElement player)
        {
            if (player == null) throw new ArgumentNullException("player");
            _player = player;
        }

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
