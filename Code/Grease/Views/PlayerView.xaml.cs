// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PlayerView.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Interaction logic for PlayerView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Views
{
	using System.Windows;

	using Grease.ViewModels;

	using ReactiveUI;

	/// <summary>
	/// Interaction logic for PlayerView.xaml
	/// </summary>
	public partial class PlayerView : IViewFor<IPlayerViewModel>
	{
		/// <summary>
		/// The view model property.
		/// </summary>
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IPlayerViewModel), typeof(PlayerView), new PropertyMetadata(null));

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerView"/> class.
		/// </summary>
		public PlayerView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		public IPlayerViewModel ViewModel
		{
			get { return (IPlayerViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		/// <summary>
		/// Gets or sets the view model.
		/// </summary>
		object IViewFor.ViewModel
		{
			get { return this.ViewModel; }
			set { this.ViewModel = (IPlayerViewModel)value; }
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
