// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TrackInfo.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the TrackInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Grease.Core
{
	/// <summary>
	/// The music file info.
	/// </summary>
	public class TrackInfo : ITrackInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TrackInfo"/> class.
		/// </summary>
		public TrackInfo()
		{
			this.Name = "Default";
			this.FileName = "Default";
			this.FullPath = "Default";
			this.Album = "Default";
			this.Artist = "Default";
			this.TrackNum = -1;
			this.HasImage = false;
			this.ImagePath = string.Empty;
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the full path.
		/// </summary>
		public string FullPath { get; set; }

		/// <summary>
		/// Gets or sets the album.
		/// </summary>
		public string Album { get; set; }

		/// <summary>
		/// Gets or sets the artist.
		/// </summary>
		public string Artist { get; set; }

		/// <summary>
		/// Gets or sets the track num.
		/// </summary>
		public int TrackNum { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether has image.
		/// </summary>
		public bool HasImage { get; set; }

		/// <summary>
		/// Gets or sets the image path.
		/// </summary>
		public string ImagePath { get; set; }
	}
}