using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Grease.Core
{
    public class WpfPlayer : IMusicPlayer
    {
        private readonly MediaElement _player;

        public WpfPlayer(MediaElement player)
        {
            if (player == null) throw new ArgumentNullException("player");
            _player = player;
        }

        public void Play(string path)
        {
            
        }

        public void Pause()
        {
            
        }

        public void ChangeVolume(double toChange)
        {
            
        }
    }
}
