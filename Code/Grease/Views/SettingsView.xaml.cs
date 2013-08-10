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
	public partial class SettingsView : IViewFor<ISettingsViewModel>
	{
		/// <summary>
		/// The view model property.
		/// </summary>
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IPlayerViewModel), typeof(PlayerView), new PropertyMetadata(null));

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
			get { return (ISettingsViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		object IViewFor.ViewModel
		{
			get { return this.ViewModel; }
			set { this.ViewModel = (ISettingsViewModel)value; }
		}

		/// <summary>
		/// The on apply template.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.DataContext = this.ViewModel;
		}
	}
}
