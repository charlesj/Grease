namespace Grease.ViewModels
{
	using Grease.Core;
	using Grease.Services;
	using Grease.Views;

	using Ninject;

	using ReactiveUI;
	using ReactiveUI.Routing;

	public class ApplicationBootstrapper
	{
		public static void ConfigureServiceLocator(ApplicationViewModel applicationViewModel, IRoutingState testRouter = null, IKernel testKernel = null)
		{
			IKernel kernel;
			if (testKernel == null)
			{
				kernel = GetStandardKernel(applicationViewModel);
			}
			else
			{
				kernel = testKernel;
			}

			RxApp.ConfigureServiceLocator(
			(iface, contract) =>
			{
				if (contract != null) return kernel.Get(iface, contract);
				return kernel.Get(iface);
			},
			(iface, contract) =>
			{
				if (contract != null) return kernel.GetAll(iface, contract);
				return kernel.GetAll(iface);
			},
			(realClass, iface, contract) =>
			{
				var binding = kernel.Bind(iface).To(realClass);
				if (contract != null) binding.Named(contract);
			});
		}

		private static IKernel GetStandardKernel(ApplicationViewModel applicationViewModel)
		{
			//setup ninject
			var kernel = new StandardKernel();
			kernel.Bind(typeof(IScreen), typeof(ApplicationViewModel)).ToConstant(applicationViewModel);
			kernel.Bind<IMusicPlayer>().To<NAudioPlayer>().InSingletonScope();
			kernel.Bind<ITrackLocater>().To<WindowsFileSystemTrackLocater>();
			kernel.Bind<IMusicEngine>().To<MusicFileEngine>();
			kernel.Bind<IMusicTagProvider>().To<TagLibInformationProvider>();
			kernel.Bind<IMusicLibrary>().To<MusicLibrary>();


			//Setup views
			kernel.Bind<IPlayerViewModel>().To<PlayerViewModel>();
			kernel.Bind<IViewFor<IPlayerViewModel>>().To<PlayerView>();

			return kernel;
		}
	}
}
