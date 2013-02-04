namespace Grease
{
	using Grease.ViewModels;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class GreaseApp
	{
		public static ApplicationViewModel ViewModel { get; private set;}

		public GreaseApp()
		{
			ViewModel = new ApplicationViewModel();
		}
	}
}
