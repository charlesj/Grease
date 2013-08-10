// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingChangedEventArgs.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The setting changed event args.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;

	/// <summary>
	/// The setting changed event args.
	/// </summary>
	public class SettingChangedEventArgs : EventArgs
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public string Value { get; set; }
	}
}