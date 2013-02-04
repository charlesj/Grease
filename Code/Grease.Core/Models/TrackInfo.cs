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

		public string Name { get; set; }

		public string FileName { get; set; }

		public string FullPath { get; set; }

		public string Album { get; set; }

		public string Artist { get; set; }

		public int TrackNum { get; set; }

		public bool HasImage { get; set; }

		public string ImagePath { get; set; }
	}
}