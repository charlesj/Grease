// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sandbox.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the Sandbox type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Tests
{
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Threading;

	using NAudio.Wave;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
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
			////wavePlayer.PlaybackStopped += wavePlayer_PlaybackStopped;
			wavePlayer.Play();

			Thread.Sleep(10000);
		}
	}
}
