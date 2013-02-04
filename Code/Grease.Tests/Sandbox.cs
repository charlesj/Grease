using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grease.Tests
{
	using System.IO;
	using System.Threading;

	using NAudio.Wave;

	using Xunit;

	public class Sandbox
    {
		[Fact]
		public void Play()
		{
			var path = Path.Combine(@"E:\Dropbox\Music\Imagine Dragons\Night Visions", "01 - Radioactive.mp3");
			var wavePlayer = new WaveOut();
			var file = new AudioFileReader(path);
			file.Volume = 0.8f;
			wavePlayer.Init(file);
			//wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
			wavePlayer.Play();

			Thread.Sleep(10000);
		}
    }
}
