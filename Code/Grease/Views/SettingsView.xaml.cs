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
	using System.Windows;

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

		public ISettingsViewModel ViewModel
		{
			get { return (ISettingsViewModel)this.DataContext; }
			set
			{
				this.DataContext = value;
			}
		}

		object IViewFor.ViewModel
		{
			get { return this.ViewModel; }
			set
			{
				this.ViewModel = (ISettingsViewModel)value;
			}
		}
	}

	public interface ISettingsView : IViewFor<ISettingsViewModel>
	{
		
	}
}
