// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettings.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the ISettings type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	/// <summary>
	/// The Settings interface.
	/// </summary>
	public interface ISettings
	{
		/// <summary>
		/// Triggered whenever a settings changes.
		/// </summary>
		event SettingChangedEventHandler OnSettingChanged;

		/// <summary>
		/// Gets or sets the root path.
		/// </summary>
		string RootPath { get; set; }

		float Volume { get; set; }
	}
}