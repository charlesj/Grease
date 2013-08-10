// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MusicLibrary.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The music library.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;
	using System.Collections.ObjectModel;

	/// <summary>
	/// The music library.
	/// </summary>
	public class MusicLibrary : IMusicLibrary
	{
		/// <summary>
		/// The locater.  Used to locate tracks from some source, most likely the file system.
		/// </summary>
		private readonly ITrackLocater locater;

		/// <summary>
		/// The _rand.
		/// </summary>
		private readonly Random rand = new Random(DateTime.Now.Millisecond);

		/// <summary>
		/// The _current song index.
		/// </summary>
		private int currentSongIndex = -1;

		/// <summary>
		/// Initializes a new instance of the <see cref="MusicLibrary"/> class.
		/// </summary>
		/// <param name="locater">
		/// The locater.
		/// </param>
		public MusicLibrary(ITrackLocater locater)
		{
			this.locater = locater;
			this.NumberToPlayBeforeRepeats = 500;
			
			this.PlayedSongs = new ObservableCollection<ITrackInfo>();
		}

		/// <summary>
		/// Gets or sets the number to play before repeats.
		/// </summary>
		public int NumberToPlayBeforeRepeats { get; set; }

		/// <summary>
		/// Gets or sets the played songs.
		/// </summary>
		public ObservableCollection<ITrackInfo> PlayedSongs { get; set; }

		/// <summary>
		/// Gets the songs in the library.
		/// </summary>
		public ObservableCollection<ITrackInfo> Songs
		{
			get
			{
				return this.locater.Found;
			}
		}

		/// <summary>
		/// The get next.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		public ITrackInfo GetNext()
		{
			if (this.currentSongIndex < this.PlayedSongs.Count)
			{
				if (this.currentSongIndex + 1 == this.PlayedSongs.Count)
				{
					this.PlayedSongs.Add(this.GetRandomMusicFile());
				}
			}

			this.currentSongIndex += 1;
			return this.PlayedSongs[this.currentSongIndex];
		}

		/// <summary>
		/// The get previous.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		public ITrackInfo GetPrevious()
		{
			if (this.currentSongIndex < 0)
			{
				if (this.PlayedSongs.Count == 0)
				{
					this.PlayedSongs.Add(this.GetRandomMusicFile());
				}

				this.currentSongIndex = 0;
				return this.PlayedSongs[this.currentSongIndex];
			}

			if (this.currentSongIndex == 0)
			{
				return this.PlayedSongs[this.currentSongIndex];
			}

			this.currentSongIndex -= 1;
			return this.PlayedSongs[this.currentSongIndex];
		}

		/// <summary>
		/// This method gets a random file from the library.  It will then check to see if it's been played recently. If it has, it gets another track.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		private ITrackInfo GetRandomMusicFile()
		{
			var test = this.Songs[this.rand.Next(this.Songs.Count - 1)];
			var min = Math.Min(this.NumberToPlayBeforeRepeats, this.PlayedSongs.Count);
			var hasPlayedRecently = true;
			while (hasPlayedRecently)
			{
				var found = false;
				for (var i = this.PlayedSongs.Count - min; i < this.PlayedSongs.Count; i++)
				{
					if (test.FullPath == this.PlayedSongs[i].FullPath)
					{
						found = true;
					}
				}

				if (!found)
				{
					hasPlayedRecently = false;
				}
				else
				{
					test = this.Songs[this.rand.Next(this.Songs.Count - 1)];
				}
			}
			
			return test;
		}
	}
}