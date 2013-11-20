// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using Grease.Core;
	using Grease.Services;
	using Grease.ViewModels;
	using Grease.Views;

	using Ninject;

	using ReactiveUI;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class GreaseApp
	{
		/// <summary>
		/// The kernel.
		/// </summary>
		private IKernel kernel;

		/// <summary>
		/// The view model.
		/// </summary>
		private IApplicationViewModel viewModel;

		/// <summary>
		/// The on startup.
		/// </summary>
		/// <param name="e">
		/// The e.
		/// </param>
		protected override void OnStartup(System.Windows.StartupEventArgs e)
		{
			base.OnStartup(e);

			this.kernel = GetStandardKernel();
			this.viewModel = new ApplicationViewModel();
			var resolver = (ModernDependencyResolver)RxApp.DependencyResolver;

			resolver.RegisterConstant(this.viewModel, typeof(IScreen));
			resolver.RegisterConstant(this.viewModel.Router, typeof(IRoutingState));

			resolver.RegisterLazySingleton(() => new PlayerView(), typeof(IPlayerView));
			resolver.RegisterLazySingleton(() => new SettingsView(), typeof(ISettingsView));

			resolver.Register(() => new MainViewModel(this.viewModel), typeof(IMainViewModel));
			resolver.Register(() => new PlayerViewModel(this.viewModel, this.kernel.Get<IMusicEngine>(), this.kernel.Get<ISettings>()), typeof(IPlayerViewModel));
			resolver.Register(() => new SettingsViewModel(this.viewModel, this.kernel.Get<ISettings>()), typeof(ISettingsViewModel));

			var welcomeVm = RxApp.DependencyResolver.GetService<IPlayerViewModel>();
			this.viewModel.Router.Navigate.Execute(welcomeVm);
		}

		/// <summary>
		/// The get standard kernel.
		/// </summary>
		/// <returns>
		/// The <see cref="IKernel"/>.
		/// </returns>
		private static IKernel GetStandardKernel()
		{
			// setup ninject
			var kernel = new StandardKernel();

			kernel.Bind<IMusicPlayer>().To<NAudioPlayer>().InSingletonScope();
			kernel.Bind<ISettings>().To<WindowSettings>().InSingletonScope();
			kernel.Bind<ITrackLocater>().To<WindowsFileSystemTrackLocater>();
			kernel.Bind<IMusicEngine>().To<MusicFileEngine>();
			kernel.Bind<IMusicTagProvider>().To<TagLibInformationProvider>();
			kernel.Bind<IMusicLibrary>().To<MusicLibrary>();

			// Setup views
			kernel.Bind<IPlayerViewModel>().To<PlayerViewModel>();
			kernel.Bind<IViewFor<IPlayerViewModel>>().To<PlayerView>();
			kernel.Bind<ISettingsViewModel>().To<SettingsViewModel>();
			kernel.Bind<IViewFor<ISettingsViewModel>>().To<SettingsView>();

			return kernel;
		}
	}
}
