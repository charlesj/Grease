// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsFileSystemTrackLocater.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Linq;

	using Grease.Core;

	/// <summary>
	/// The windows file system access.
	/// </summary>
	public class WindowsFileSystemTrackLocater : ITrackLocater
	{
		/// <summary>
		/// The _provider.
		/// </summary>
		private readonly IMusicTagProvider provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="WindowsFileSystemTrackLocater"/> class.
		/// </summary>
		/// <param name="provider">
		/// The provider.
		/// </param>
		public WindowsFileSystemTrackLocater(IMusicTagProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>
		/// The get music files.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <returns>
		/// The System.Collections.Generic.List`1[T -&gt; Grease.Core.MusicFileInfo].
		/// </returns>
		public ObservableCollection<ITrackInfo> GetMusicFiles(string path)
		{
			var playableExtensions = new List<string> { "*.mp3", "*.m4a" };
			var allSongs = new List<ITrackInfo>();
			var directories = Directory.GetDirectories(path);
			foreach (var extension in playableExtensions)
			{
				var musicFiles = Directory.GetFiles(path, extension);
				allSongs.AddRange(musicFiles.Select(this.GetMusicFileInfo));
			}

			foreach (var directory in directories)
			{
				allSongs = allSongs.Concat(this.GetMusicFiles(directory)).ToList();
			}

			return new ObservableCollection<ITrackInfo>(allSongs);
		}

		/// <summary>
		/// The get music file info.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <returns>
		/// The Grease.Core.MusicFileInfo.
		/// </returns>
		private TrackInfo GetMusicFileInfo(string path)
		{
			return new TrackInfo { FullPath = path };
		}
	}
}