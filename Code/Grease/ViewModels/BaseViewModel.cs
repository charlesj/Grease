// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the BaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using ReactiveUI;

	/// <summary>
	/// The base view model.
	/// </summary>
	public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BaseViewModel"/> class.
		/// </summary>
		/// <param name="screen">
		/// The screen.
		/// </param>
		protected BaseViewModel(IScreen screen)
		{
			this.HostScreen = screen;
		}

		/// <summary>
		/// Gets the url path segment.
		/// </summary>
		public abstract string UrlPathSegment { get; }

		/// <summary>
		/// Gets the host screen.
		/// </summary>
		public IScreen HostScreen { get; private set; }

		/// <summary>
		/// The write to status bar.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		protected void WriteToStatusBar(string message)
		{
			var appViewModel = this.HostScreen as IApplicationViewModel;
			if (appViewModel != null)
			{
				appViewModel.WriteToStatusBar(message);
			}
		}
	}
}