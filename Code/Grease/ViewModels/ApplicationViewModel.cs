// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the ApplicationViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;

	using Grease.Core;

	using Ninject;

	using ReactiveUI;
	using ReactiveUI.Routing;
	using ReactiveUI.Xaml;

	/// <summary>
	/// The application view model.
	/// </summary>
	public class ApplicationViewModel : IScreen
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationViewModel"/> class.
		/// </summary>
		/// <param name="testRouter">
		/// The test router.
		/// </param>
		/// <param name="testKernel">
		/// The test kernel.
		/// </param>
		public ApplicationViewModel(IRoutingState testRouter = null, IKernel testKernel = null)
		{
			this.Router = testRouter ?? new RoutingState();
			ApplicationBootstrapper.ConfigureServiceLocator(this, testRouter, testKernel);
			
			var initialViewModel = RxApp.GetService<IPlayerViewModel>();
			this.Router.Navigate.Execute(initialViewModel);

			this.GoToSettings = new ReactiveCommand();
			this.GoToSettings.Subscribe(param => this.ShowSettings());
		}

		/// <summary>
		/// The show settings.
		/// </summary>
		private void ShowSettings()
		{
			this.Router.Navigate.Execute(RxApp.GetService<ISettingsViewModel>());
		}

		/// <summary>
		/// Gets the router.
		/// </summary>
		public IRoutingState Router { get; private set; }

		/// <summary>
		/// Gets the song opened command.
		/// </summary>
		public ReactiveCommand GoToSettings { get; private set; }
	}
}
