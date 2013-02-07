// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMusicEngine.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the IMusicEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;

	/// <summary>
	/// The MusicEngine interface.
	/// </summary>
	public interface IMusicEngine
	{
		/// <summary>
		/// Gets or sets a value indicating whether is playing.
		/// </summary>
		bool CurrentlyPlaying { get; set; }

		/// <summary>
		/// Gets the found count.
		/// </summary>
		int FoundCount { get; }

		/// <summary>
		/// Gets the library.
		/// </summary>
		IMusicLibrary Library { get; }

		/// <summary>
		/// Gets the current.
		/// </summary>
		ITrackInfo Current { get; }

		/// <summary>
		/// Gets or sets the elapsed on the current track.
		/// </summary>
		TimeSpan Elapsed { get; set; }

		/// <summary>
		/// Gets or sets the total time of the current track.
		/// </summary>
		TimeSpan TotalTime { get; set; }

		/// <summary>
		/// The play.
		/// </summary>
		/// <param name="next">
		/// The next.
		/// </param>
		void Play(bool next);

		/// <summary>
		/// The pause.
		/// </summary>
		void Pause();

		/// <summary>
		/// The next.
		/// </summary>
		void Next();

		/// <summary>
		/// The previous.
		/// </summary>
		void Previous();

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The new volume.
		/// </param>
		void ChangeVolume(float newVolume);
	}
}