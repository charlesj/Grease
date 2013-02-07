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
		/// The track ended.
		/// </summary>
		event TrackEndedEventHandler TrackEnded;

		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		string Source { get; set; }

		/// <summary>
		/// Gets the length value.
		/// </summary>
		float LengthValue { get; }

		/// <summary>
		/// Gets the elapsed.
		/// </summary>
		TimeSpan Elapsed { get; }

		/// <summary>
		/// Gets the total time.
		/// </summary>
		TimeSpan TotalTime { get; }

		/// <summary>
		/// The scrub.
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