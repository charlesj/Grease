// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;

	using ReactiveUI;

	/// <summary>
	/// The application view model.
	/// </summary>
	public class MainViewModel : ReactiveObject, IMainViewModel
	{
		/// <summary>
		/// The screen.
		/// </summary>
		private readonly IScreen screen;

		private string statusText;

		private IApplicationViewModel applicationViewModel;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		/// <param name="screen">
		/// The screen.
		/// </param>
		public MainViewModel(IScreen screen)
		{
			this.screen = screen;
			this.GoToSettings = new ReactiveCommand();
			this.GoToSettings.Subscribe(param => this.ShowSettings());

			this.applicationViewModel = this.screen as IApplicationViewModel;
			this.applicationViewModel.ObservableForProperty(model => model.StatusBarText)
									 .Subscribe(param => this.StatusText = param.Value);
			this.StatusText = this.applicationViewModel.StatusBarText;
		}

		/// <summary>
		/// Gets the song opened command.
		/// </summary>
		public ReactiveCommand GoToSettings { get; private set; }

		/// <summary>
		/// Gets or sets the text in the status bar.
		/// </summary>
		public string StatusText
		{
			get
			{
				return this.statusText;
			}
			set
			{
				this.RaiseAndSetIfChanged(ref this.statusText, value);
			}
		}

		/// <summary>
		/// The show settings.
		/// </summary>
		private void ShowSettings()
		{
			this.screen.Router.Navigate.Execute(RxApp.DependencyResolver.GetService<ISettingsViewModel>());
		}
	}
}
