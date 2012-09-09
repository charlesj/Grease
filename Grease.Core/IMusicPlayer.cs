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
	/// <summary>
	/// The MusicPlayer interface.
	/// </summary>
	public interface IMusicPlayer
    {
		/// <summary>
		/// Gets or sets the source.
		/// </summary>
		string Source { get; set; }

		/// <summary>
		/// The change volume.
		/// </summary>
		/// <param name="newVolume">
		/// The new Volume.
		/// </param>
		void ChangeVolume(double newVolume);

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