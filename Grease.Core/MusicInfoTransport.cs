// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicInfoTransport.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the MusicInfoTransport type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Grease.Core
{
	/// <summary>
	/// The music info transport.
	/// </summary>
	public class MusicInfoTransport : IMusicFileInfo
	{
		/// <summary>
		/// Gets or sets the album.
		/// </summary>
		public string Album { get; set; }

		/// <summary>
		/// Gets or sets the artist.
		/// </summary>
		public string Artist { get; set; }

		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the full path.
		/// </summary>
		public string FullPath { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether has image.
		/// </summary>
		public bool HasImage { get; set; }

		/// <summary>
		/// Gets or sets the image path.
		/// </summary>
		public string ImagePath { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the track num.
		/// </summary>
		public int TrackNum { get; set; }
	}
}