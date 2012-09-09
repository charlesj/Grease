// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicFileInfo.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the MusicFileInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Grease.Core
{
	using System;

	/// <summary>
	/// The music file info.
	/// </summary>
	public class MusicFileInfo : IMusicFileInfo
	{
		/// <summary>
		/// The _tag provider.
		/// </summary>
		private readonly IMusicTagProvider tagProvider;

		/// <summary>
		/// The _artist.
		/// </summary>
		private string artist;

		/// <summary>
		/// The _track num.
		/// </summary>
		private int trackNum;

		/// <summary>
		/// The album.
		/// </summary>
		private string album;

		/// <summary>
		/// The file name.
		/// </summary>
		private string fileName;

		/// <summary>
		/// The have tried to load.
		/// </summary>
		private bool haveTriedToLoad;

		/// <summary>
		/// The name.
		/// </summary>
		private string name;

		/// <summary>
		/// Initializes a new instance of the <see cref="MusicFileInfo"/> class.
		/// </summary>
		/// <param name="tagProvider">
		/// The tag provider.
		/// </param>
		/// <exception cref="ArgumentNullException">
		/// Thrown if tagProvider is null.
		/// </exception>
		public MusicFileInfo(IMusicTagProvider tagProvider)
		{
			if (tagProvider != null)
			{
				this.tagProvider = tagProvider;
			}
			else
			{
				throw new ArgumentNullException("tagProvider");
			}

			this.haveTriedToLoad = false;
		}

		/// <summary>
		/// Gets or sets the album.
		/// </summary>
		public string Album
		{
			get
			{
				this.LoadAdditionalInfo();
				return this.album;
			}

			set
			{
				this.album = value;
			}
		}

		/// <summary>
		/// Gets or sets the artist.
		/// </summary>
		public string Artist
		{
			get
			{
				this.LoadAdditionalInfo();
				return this.artist;
			}

			set
			{
				this.artist = value;
			}
		}

		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		public string FileName
		{
			get
			{
				this.LoadAdditionalInfo();
				return this.fileName;
			}

			set
			{
				this.fileName = value;
			}
		}

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
		public string Name
		{
			get
			{
				this.LoadAdditionalInfo();
				return this.name;
			}

			set
			{
				this.name = value;
			}
		}

		/// <summary>
		/// Gets or sets the track num.
		/// </summary>
		public int TrackNum
		{
			get
			{
				this.LoadAdditionalInfo();
				return this.trackNum;
			}

			set
			{
				this.trackNum = value;
			}
		}

		/// <summary>
		/// The load additional info.
		/// </summary>
		public void LoadAdditionalInfo()
		{
			if (!this.haveTriedToLoad)
			{
				IMusicFileInfo loaded = this.tagProvider.GetInfo(this.FullPath);
				this.Album = loaded.Album;
				this.Artist = loaded.Artist;
				this.Name = loaded.Name;
				this.FileName = loaded.FileName;
				this.HasImage = loaded.HasImage;
				this.ImagePath = loaded.ImagePath;
				this.TrackNum = loaded.TrackNum;
				this.haveTriedToLoad = true;
			}
		}
	}
}