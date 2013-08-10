// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowSettings.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the WindowSettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Services
{
	using Grease.Core;

	/// <summary>
	/// The window settings.
	/// </summary>
	public class WindowSettings : Settings
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WindowSettings"/> class.
		/// </summary>
		public WindowSettings()
		{
			this.RootPath = Properties.Settings.Default.MusicFilePath;
		}
	}
}