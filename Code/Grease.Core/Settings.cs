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
	/// <summary>
	/// The settings.
	/// </summary>
	public class Settings : ISettings
	{
		/// <summary>
		/// The root path.
		/// </summary>
		private string rootPath;

		/// <summary>
		/// Triggered whenever a settings changes.
		/// </summary>
		public event SettingChangedEventHandler OnSettingChanged;

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