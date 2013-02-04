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
	using System.Linq;

	public interface IMusicLibrary
	{
		/// <summary>
		/// Gets or sets the played songs.
		/// </summary>
		ObservableCollection<ITrackInfo> PlayedSongs { get; set; }

		/// <summary>
		/// Gets or sets the songs.
		/// </summary>
		ObservableCollection<ITrackInfo> Songs { get; set; }

		/// <summary>
		/// The get next.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		ITrackInfo GetNext();

		/// <summary>
		/// The get previous.
		/// </summary>
		/// <returns>
		/// The Grease.Core.TrackInfo.
		/// </returns>
		ITrackInfo GetPrevious();
	}

	/// <summary>
	/// The music library.
	/// </summary>
	public class MusicLibrary : IMusicLibrary
	{
		private readonly ITrackLocater locater;

		private readonly IMusicTagProvider provider;

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
		public MusicLibrary(ITrackLocater locater, IMusicTagProvider provider)
		{
			this.locater = locater;
			this.provider = provider;
			this.NumberToPlayBeforeRepeats = 500;
			this.Songs = this.locater.GetMusicFiles(@"E:\Dropbox\Music\Muse\The 2nd Law [Explicit]");
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
		/// Gets or sets the songs.
		/// </summary>
		public ObservableCollection<ITrackInfo> Songs { get; set; }

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
		/// The get random music file.
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

			if (test.Name == "Default")
			{
				//try loading
				var result = this.provider.GetInfo(test.FullPath);
				this.Songs.Remove(test);
				this.Songs.Add(result);
				return result;
			}

			return test;
		}
	}
}