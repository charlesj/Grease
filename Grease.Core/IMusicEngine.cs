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
		MusicLibrary Library { get; }

		/// <summary>
		/// Gets the current.
		/// </summary>
		CurrentlyPlayingViewModel Current { get; }

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
		void ChangeVolume(double newVolume);

		/// <summary>
		/// The load.
		/// </summary>
		/// <param name="path">
		/// The path.
		/// </param>
		void Load(string path);
	}
}