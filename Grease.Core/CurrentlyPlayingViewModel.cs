// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentlyPlayingViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the CurrentlyPlayingViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	/// <summary>
	/// The currently playing view model.
	/// </summary>
	public class CurrentlyPlayingViewModel
	{
		/// <summary>
		/// Gets or sets the name of the currently playing song.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the album of the currently playing song.
		/// </summary>
		public string Album { get; set; }

		/// <summary>
		/// Gets or sets the track number  of the currently playing song.
		/// </summary>
		public int TrackNum { get; set; }

		/// <summary>
		/// Gets or sets the file name of the currently playing song.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the currently playing song has an image.
		/// </summary>
		public bool HasImage { get; set; }

		/// <summary>
		/// Gets or sets the image path of the currently playing song.
		/// </summary>
		public string ImagePath { get; set; }

		/// <summary>
		/// Gets or sets the artist of the currently playing song.
		/// </summary>
		public string Artist { get; set; }
	}
}
