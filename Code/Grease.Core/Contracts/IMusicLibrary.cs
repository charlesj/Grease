// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMusicLibrary.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The MusicLibrary interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System.Collections.ObjectModel;

	/// <summary>
	/// The MusicLibrary interface.
	/// </summary>
	public interface IMusicLibrary
	{
		/// <summary>
		/// Gets or sets the played songs.
		/// </summary>
		ObservableCollection<ITrackInfo> PlayedSongs { get; set; }

		/// <summary>
		/// Gets or sets the songs.
		/// </summary>
		ObservableCollection<ITrackInfo> Songs { get; set; }

		/// <summary>
		/// Returns the next track, or chooses one randomly.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		ITrackInfo GetNext();

		/// <summary>
		/// Returns the previous track.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		ITrackInfo GetPrevious();
	}
}