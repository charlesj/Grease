// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsFileSystemAccess.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using Grease.Core;

	/// <summary>
	/// The windows file system access.
	/// </summary>
	public class WindowsFileSystemAccess : IGreaseFileSystemAccess
	{
		/// <summary>
		/// The _provider.
		/// </summary>
		private readonly IMusicTagProvider provider;

		/// <summary>
		/// Initializes a new instance of the <see cref="WindowsFileSystemAccess"/> class.
		/// </summary>
		/// <param name="provider">
		/// The provider.
		/// </param>
		public WindowsFileSystemAccess(IMusicTagProvider provider)
		{
			this.provider = provider;
		}

		/// <summary>
		/// The get albums.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <returns>
		/// The System.Collections.Generic.List`1[T -&gt; Grease.Core.AlbumInfo].
		/// </returns>
		public List<AlbumInfo> GetAlbums(string path)
		{
			throw new NotImplementedException();
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
		public List<MusicFileInfo> GetMusicFiles(string path)
		{
			var playableExtensions = new List<string> { "*.mp3", "*.m4a" };
			var rtn = new List<MusicFileInfo>();
			var directories = Directory.GetDirectories(path);
			foreach (var extension in playableExtensions)
			{
				var musicFiles = Directory.GetFiles(path, extension);
				rtn.AddRange(musicFiles.Select(this.GetMusicFileInfo));
			}

			foreach (var directory in directories)
			{
				rtn = rtn.Concat(this.GetMusicFiles(directory)).ToList();
			}

			return rtn;
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
		private MusicFileInfo GetMusicFileInfo(string path)
		{
			return new MusicFileInfo(this.provider) { FullPath = path };
		}
	}
}