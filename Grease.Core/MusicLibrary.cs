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
	using System.Collections.Generic;

	/// <summary>
	/// The music library.
	/// </summary>
	public class MusicLibrary
	{
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
		/// <param name="numberToPlayBeforeRepeats">
		/// The number to play before repeats.
		/// </param>
		public MusicLibrary(int numberToPlayBeforeRepeats = 500)
		{
			this.NumberToPlayBeforeRepeats = numberToPlayBeforeRepeats;
			this.Songs = new List<MusicFileInfo>();
			this.PlayedSongs = new List<MusicFileInfo>();
		}

		/// <summary>
		/// Gets or sets the number to play before repeats.
		/// </summary>
		public int NumberToPlayBeforeRepeats { get; set; }

		/// <summary>
		/// Gets or sets the played songs.
		/// </summary>
		public List<MusicFileInfo> PlayedSongs { get; set; }

		/// <summary>
		/// Gets or sets the songs.
		/// </summary>
		public List<MusicFileInfo> Songs { get; set; }

		/// <summary>
		/// The get next.
		/// </summary>
		/// <returns>
		/// The Grease.Core.MusicFileInfo.
		/// </returns>
		public MusicFileInfo GetNext()
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
		/// The Grease.Core.MusicFileInfo.
		/// </returns>
		public MusicFileInfo GetPrevious()
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
		/// The get random music file.
		/// </summary>
		/// <returns>
		/// The Grease.Core.MusicFileInfo.
		/// </returns>
		private MusicFileInfo GetRandomMusicFile()
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