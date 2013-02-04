// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITrackInfo.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the ITrackInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	/// <summary>
	/// The TrackInfo interface.
	/// </summary>
	public interface ITrackInfo
    {
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		string FileName { get; set; }

		/// <summary>
		/// Gets or sets the full path.
		/// </summary>
		string FullPath { get; set; }

		/// <summary>
		/// Gets or sets the album.
		/// </summary>
		string Album { get; set; }

		/// <summary>
		/// Gets or sets the artist.
		/// </summary>
		string Artist { get; set; }

		/// <summary>
		/// Gets or sets the track num.
		/// </summary>
		int TrackNum { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether has image.
		/// </summary>
		bool HasImage { get; set; }

		/// <summary>
		/// Gets or sets the image path.
		/// </summary>
		string ImagePath { get; set; }
    }
}