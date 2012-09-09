// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMusicFileInfo.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the IMusicFileInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	/// <summary>
	/// The MusicFileInfo interface.
	/// </summary>
	public interface IMusicFileInfo
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