// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrackLocater.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the ITrackLocater type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System.Collections.ObjectModel;

	/// <summary>
	/// The GreaseFileSystemAccess interface.
	/// </summary>
	public interface ITrackLocater
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
		ObservableCollection<ITrackInfo> GetMusicFiles(string path);
    }
}