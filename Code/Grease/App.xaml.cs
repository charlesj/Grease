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
	using Grease.ViewModels;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class GreaseApp
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GreaseApp"/> class.
		/// </summary>
		public GreaseApp()
		{
			ViewModel = new ApplicationViewModel();
		}

		/// <summary>
		/// Gets the view model.
		/// </summary>
		public static ApplicationViewModel ViewModel { get; private set; }
	}
}
