// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   The application view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using ReactiveUI;

	/// <summary>
	/// The application view model.
	/// </summary>
	public class ApplicationViewModel : ReactiveObject, IApplicationViewModel
	{
		/// <summary>
		/// The status bar text.
		/// </summary>
		private string statusBarText;

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
		/// </summary>
		public ApplicationViewModel()
		{
			this.Router = new RoutingState();
		}

		/// <summary>
		/// Gets the router.
		/// </summary>
		public IRoutingState Router { get; private set; }

		/// <summary>
		/// Gets the status bar text.
		/// </summary>
		public string StatusBarText
		{
			get
			{
				return this.statusBarText;
			}

			set
			{
				this.RaisePropertyChanged("StatusBarText");
				this.RaiseAndSetIfChanged(ref this.statusBarText, value);
			}
		}

		/// <summary>
		/// The write to status bar.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public void WriteToStatusBar(string message)
		{
			this.StatusBarText = message;
		}
	}
}