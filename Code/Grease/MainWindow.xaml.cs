namespace Grease
{
	using ReactiveUI;
	using ReactiveUI.Routing;

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			viewHost.Router = RxApp.GetService<IScreen>().Router;
			this.DataContext = GreaseApp.ViewModel;
		}
	}
}
