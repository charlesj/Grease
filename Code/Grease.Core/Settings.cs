// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Core
{
	using System;

	/// <summary>
	/// The settings.
	/// </summary>
	public class Settings : ISettings
	{
		/// <summary>
		/// The root path.
		/// </summary>
		private string rootPath;

		private float volume;

		/// <summary>
		/// Triggered whenever a settings changes.
		/// </summary>
		public event SettingChangedEventHandler OnSettingChanged;

		/// <summary>
		/// Enables persisting of the Volume level between play sessions.
		/// </summary>
		public float Volume
		{
			get
			{
				return this.volume;
			}
			set
			{
				if (Math.Abs(this.volume - value) > 0.1 && value >= 0)
				{
					this.volume = value;
					if (this.OnSettingChanged != null)
					{
						this.OnSettingChanged(new SettingChangedEventArgs { Name="Volume", Value = this.volume.ToString() });
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the root path.
		/// </summary>
		public string RootPath
		{
			get
			{
				return this.rootPath;
			}

			set
			{
				if (string.IsNullOrEmpty(this.rootPath) || this.rootPath != value)
				{
					this.rootPath = value;
					if (this.OnSettingChanged != null)
					{
						this.OnSettingChanged(new SettingChangedEventArgs { Name = "RootPath", Value = this.rootPath });
					}
				}
			}
		}
	}
}