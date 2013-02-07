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
			viewHost.Router = RxApp.GetService<IScreen>().Router;
			this.DataContext = GreaseApp.ViewModel;
		}
	}
}
