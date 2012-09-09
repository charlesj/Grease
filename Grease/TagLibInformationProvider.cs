// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TagLibInformationProvider.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using Grease.Core;

	using File = TagLib.File;

	/// <summary>
	/// The tag lib information provider.
	/// </summary>
	public class TagLibInformationProvider : IMusicTagProvider
	{
		/// <summary>
		/// The get info.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <returns>
		/// The Grease.Core.IMusicFileInfo.
		/// </returns>
		public IMusicFileInfo GetInfo(string path)
		{
			var rtn = new MusicInfoTransport();
			var fileInfo = new FileInfo(path);
			rtn.FullPath = fileInfo.FullName;
			rtn.FileName = fileInfo.Name;
			rtn.ImagePath = this.GetImage(fileInfo);
			if (!string.IsNullOrEmpty(rtn.ImagePath))
			{
				rtn.HasImage = true;
			}

			var tags = File.Create(path);
			rtn.Album = tags.Tag.Album;
			rtn.Artist = tags.Tag.JoinedAlbumArtists;
			rtn.Name = tags.Tag.Title;
			rtn.TrackNum = (int)tags.Tag.Track;

			if (string.IsNullOrEmpty(rtn.Name))
			{
				rtn.Name = rtn.FileName;
			}

			return rtn;
		}

		/// <summary>
		/// The get image.
		/// </summary>
		/// <param name="info">
		/// The info.
		/// </param>
		/// <returns>
		/// The System.String.
		/// </returns>
		private string GetImage(FileInfo info)
		{
			var usableExtensions = new List<string> { "*.jpg", "*.png" };
			var candidates = new List<FileInfo>();
			foreach (var ext in usableExtensions)
			{
				if (info.Directory != null)
				{
					candidates.AddRange(info.Directory.GetFiles(ext));
				}
			}

			var final = candidates.OrderByDescending(fi => fi.Length).FirstOrDefault();
			if (final != null)
			{
				return final.FullName;
			}

			return string.Empty;
		}
	}
}