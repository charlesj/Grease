// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System;

	using Grease.ViewModels;

	using MahApps.Metro;

	using ReactiveUI;
	using ReactiveUI.Xaml;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			ThemeManager.ChangeTheme(
				this, new Accent("GreaseTheme", new Uri("pack://application:,,,/Grease;component/Accents/GreaseAccent.xaml")), Theme.Light);
			var viewHost = new RoutedViewHost();
			this.Presenter.Content = viewHost;

			var screen = RxApp.DependencyResolver.GetService<IScreen>();
			viewHost.Router = screen.Router;

			DataContext = RxApp.DependencyResolver.GetService<IMainViewModel>();
		}

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}
