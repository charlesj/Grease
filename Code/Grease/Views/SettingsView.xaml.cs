// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsView.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Interaction logic for SettingsView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Views
{
	using Grease.ViewModels;

	using ReactiveUI;

	/// <summary>
	/// Interaction logic for SettingsView.xaml
	/// </summary>
	public partial class SettingsView : ISettingsView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsView"/> class. 
		/// </summary>
		public SettingsView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		public ISettingsViewModel ViewModel
		{
			get
			{
				return (ISettingsViewModel)this.DataContext;
			}

			set
			{
				this.DataContext = value;
			}
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		object IViewFor.ViewModel
		{
			get
			{
				return this.ViewModel;
			}

			set
			{
				this.ViewModel = (ISettingsViewModel)value;
			}
		}
	}
}
