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

	using MahApps.Metro;

	using ReactiveUI;
	using ReactiveUI.Routing;

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

			this.ViewHost.Router = RxApp.GetService<IScreen>().Router;
			this.DataContext = GreaseApp.ViewModel;
		}
	}
}
