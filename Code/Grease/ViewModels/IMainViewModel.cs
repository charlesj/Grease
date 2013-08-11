// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMainViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The MainViewModel interface.  This is the view model on the main window
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using ReactiveUI;

	/// <summary>
	/// The MainViewModel interface.  This is the view model on the main window
	/// </summary>
	public interface IMainViewModel
	{
		/// <summary>
		/// Gets the go to settings.
		/// </summary>
		ReactiveCommand GoToSettings { get; }

		/// <summary>
		/// Gets or sets the text in the status bar.
		/// </summary>
		string StatusText { get; set; }
	}
}