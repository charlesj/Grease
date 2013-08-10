// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMusicPlayer.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the IMusicPlayer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;
	using System.ComponentModel;

	/// <summary>
	/// The MusicPlayer interface.
	/// </summary>
	public interface IMusicPlayer : INotifyPropertyChanged
	{
		/// <summary>
		/// Should be triggered when a track ends.
		/// </summary>
		event TrackEndedEventHandler TrackEnded;

		/// <summary>
		/// Gets or sets the source.  The source is the path to the currently playing file.
		/// </summary>
		string Source { get; set; }

		/// <summary>
		/// Gets the length value.  This is the length of the song represented as a float.
		/// </summary>
		float LengthValue { get; }

		/// <summary>
		/// Gets the elapsed.  This is how far the song has played so far.
		/// </summary>
		TimeSpan Elapsed { get; }

		/// <summary>
		/// Gets the total time.  This is the total length of the current song.
		/// </summary>
		TimeSpan TotalTime { get; }

		/// <summary>
		/// The scrub.  This would be used to change the point in time the song is at.
		/// </summary>
		/// <param name="value">
		/// The value.
		/// </param>
		void Scrub(double value);

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The new Volume.
		/// </param>
		void ChangeVolume(float newVolume);

		/// <summary>
		/// The play.
		/// </summary>
		void Play();

		/// <summary>
		/// The pause.
		/// </summary>
		void Pause();
	}
}