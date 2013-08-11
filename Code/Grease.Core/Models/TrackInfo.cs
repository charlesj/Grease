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
		/// The provider.
		/// </summary>
		private readonly IMusicTagProvider provider;

		/// <summary>
		/// Whether the tags have been loaded from the file system.
		/// </summary>
		private bool hasLoadedTags;

		/// <summary>
		/// The full path.
		/// </summary>
		private string fullPath;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrackInfo"/> class.
		/// </summary>
		/// <param name="provider">
		/// The tag provider.  It is injected into the class so that it can lazily load the information needed.
		/// </param>
		public TrackInfo(IMusicTagProvider provider)
		{
			this.provider = provider;
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
		public string FullPath
		{
			get
			{
				this.CheckLazyLoading();
				return this.fullPath;
			}

			set
			{
				this.fullPath = value;
			}
		}

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

		private void CheckLazyLoading()
		{
			if (!this.hasLoadedTags)
			{
				this.provider.LazyLoad(this, this.fullPath);
				this.hasLoadedTags = true;
			}
		}
	}
}