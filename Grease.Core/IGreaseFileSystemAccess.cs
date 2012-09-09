// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGreaseFileSystemAccess.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the IGreaseFileSystemAccess type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System.Collections.Generic;

	/// <summary>
	/// The GreaseFileSystemAccess interface.
	/// </summary>
	public interface IGreaseFileSystemAccess
    {
		/// <summary>
		/// Gets the music files for a given path
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		/// <returns>
		/// The List of music files for the path
		/// </returns>
		List<MusicFileInfo> GetMusicFiles(string path);

		/// <summary>
		/// The get albums for a given path.
		/// </summary>
		/// <param name="path">
		/// The path to search.
		/// </param>
		/// <returns>
		/// The list of albums
		/// </returns>
		List<AlbumInfo> GetAlbums(string path);
    }
}