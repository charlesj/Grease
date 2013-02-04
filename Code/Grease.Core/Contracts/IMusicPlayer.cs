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
		/// Gets or sets the source.
		/// </summary>
		string Source { get; set; }

		float LengthValue { get; }

		TimeSpan Elapsed { get; }

		TimeSpan TotalTime { get; }

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

		event TrackEndedEventHandler TrackEnded;
    }
}