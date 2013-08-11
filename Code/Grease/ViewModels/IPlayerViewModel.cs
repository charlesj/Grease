// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPlayerViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The PlayerViewModel interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;

	using ReactiveUI;

	/// <summary>
	/// The PlayerViewModel interface.
	/// </summary>
	public interface IPlayerViewModel : IRoutableViewModel
	{
		/// <summary>
		/// Gets or sets the elapsed.
		/// </summary>
		TimeSpan Elapsed { get; set; }

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		TimeSpan TotalTime { get; set; }

		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		float Volume { get; set; }

		/// <summary>
		/// Gets or sets the current song name.
		/// </summary>
		string CurrentSongName { get; set; }

		/// <summary>
		/// Gets or sets the current artist name.
		/// </summary>
		string CurrentArtistName { get; set; }

		/// <summary>
		/// Gets or sets the current album name.
		/// </summary>
		string CurrentAlbumName { get; set; }

		/// <summary>
		/// Gets or sets the status text.
		/// </summary>
		string StatusText { get; set; }

		/// <summary>
		/// Gets or sets the timeline location.
		/// </summary>
		double TimelineLocation { get; set; }

		/// <summary>
		/// Gets the play command.
		/// </summary>
		ReactiveCommand PlayCommand { get; }

		/// <summary>
		/// Gets the pause command.
		/// </summary>
		ReactiveCommand PauseCommand { get; }

		/// <summary>
		/// Gets the next song command.
		/// </summary>
		ReactiveCommand NextSongCommand { get; }

		/// <summary>
		/// Gets the previous song command.
		/// </summary>
		ReactiveCommand PreviousSongCommand { get; }

		/// <summary>
		/// Gets the change song location command.
		/// </summary>
		ReactiveCommand ChangeSongLocationCommand { get; }

		/// <summary>
		/// Gets the load songs command.
		/// </summary>
		ReactiveCommand LoadSongsCommand { get; }

		/// <summary>
		/// Gets the song ended command.
		/// </summary>
		ReactiveCommand SongEndedCommand { get; }

		/// <summary>
		/// Gets the song opened command.
		/// </summary>
		ReactiveCommand SongOpenedCommand { get; }
	}
}