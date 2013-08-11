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
	public partial class GreaseApp : IScreen
	{
		/// <summary>
		/// Gets the router.
		/// </summary>
		public IRoutingState Router { get; private set; }

		/// <summary>
		/// Gets the view model.
		/// </summary>
		public static MainViewModel ViewModel { get; private set; }

		/// <summary>
		/// Gets the kernel.
		/// </summary>
		public static IKernel Kernel { get; private set; }

		/// <summary>
		/// The configure service locator.
		/// </summary>
		public void ConfigureServiceLocator()
		{
			Kernel = GetStandardKernel();



			////resolver.RegisterLazySingleton(() => new SettingsView(), typeof(IViewFor<ISettingsViewModel>));
			////resolver.RegisterLazySingleton(() => new PlayerView(), typeof(IViewFor<IPlayerViewModel>));

			////resolver.Register(() => new PlayerViewModel(this, Kernel.Get<IMusicEngine>()), typeof(IPlayerViewModel));
			////resolver.Register(() => new SettingsViewModel(this), typeof(ISettingsViewModel));
		}

		protected override void OnStartup(System.Windows.StartupEventArgs e)
		{
			base.OnStartup(e);

			this.Router = new RoutingState();

			var resolver = (ModernDependencyResolver)RxApp.DependencyResolver;

			resolver.RegisterConstant(this, typeof(IScreen));
			resolver.RegisterConstant(this.Router, typeof(IRoutingState));

			resolver.RegisterLazySingleton(() => new TestingView(), typeof(ITestingView));
			resolver.RegisterLazySingleton(() => new PlayerView(), typeof(IPlayerView));
			resolver.RegisterLazySingleton(() => new SettingsView(), typeof(ISettingsView));

			resolver.Register(() => new MainViewModel(this), typeof(IMainViewModel));
			resolver.Register(() => new TestingViewModel(this), typeof(ITestingViewModel));
			resolver.Register(() => new PlayerViewModel(this), typeof(IPlayerViewModel));
			resolver.Register(() => new SettingsViewModel(this), typeof(ISettingsViewModel));

			var welcomeVm = RxApp.DependencyResolver.GetService<ISettingsViewModel>();
			//var welcomeVm = RxApp.DependencyResolver.GetService<ITestingViewModel>();
			this.Router.Navigate.Execute(welcomeVm);
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
			////kernel.Bind(typeof(IScreen), typeof(MainViewModel)).ToConstant(applicationViewModel);
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
