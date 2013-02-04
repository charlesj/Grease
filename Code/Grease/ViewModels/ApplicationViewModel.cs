namespace Grease.ViewModels
{
	using Ninject;

	using ReactiveUI;
	using ReactiveUI.Routing;

	public class ApplicationViewModel : IScreen
	{
		public ApplicationViewModel(IRoutingState testRouter = null, IKernel testKernel = null)
		{
			this.Router = testRouter ?? new RoutingState();
			ApplicationBootstrapper.ConfigureServiceLocator(this, testRouter, testKernel);
			
			var initialViewModel = RxApp.GetService<IPlayerViewModel>();
			Router.Navigate.Execute(initialViewModel);
		}

		public IRoutingState Router { get; private set; }
	}
}
