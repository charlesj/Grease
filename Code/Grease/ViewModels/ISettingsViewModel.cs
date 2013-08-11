// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISettingsViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The SettingsViewModel interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using ReactiveUI;

	/// <summary>
	/// The SettingsViewModel interface.
	/// </summary>
	public interface ISettingsViewModel : IRoutableViewModel
	{
		/// <summary>
		/// Gets the root path of the music library.
		/// </summary>
		string RootPath { get; }

		/// <summary>
		/// Gets the change directory command, which handles changing the music library directory.
		/// </summary>
		IReactiveCommand ChangeDirectoryCommand { get; }

		/// <summary>
		/// Gets the go back command, which navigates the application back to the main viewer.
		/// </summary>
		IReactiveCommand GoBackCommand { get; }
	}
}