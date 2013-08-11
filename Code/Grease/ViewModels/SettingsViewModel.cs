// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the SettingsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using ReactiveUI;

	public interface ISettingsViewModel : IRoutableViewModel
	{
	}

	/// <summary>
	/// The settings view model.
	/// </summary>
	public class SettingsViewModel : ReactiveObject, ISettingsViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
		/// </summary>
		/// <param name="hostScreen">
		/// The host screen.
		/// </param>
		public SettingsViewModel(IScreen hostScreen)
		{
			this.HostScreen = hostScreen;
		}

		/// <summary>
		/// Gets the url path segment.
		/// </summary>
		public string UrlPathSegment
		{
			get
			{
				return "Settings";
			}
		}

		/// <summary>
		/// Gets the host screen.
		/// </summary>
		public IScreen HostScreen { get; private set; }
	}
}
