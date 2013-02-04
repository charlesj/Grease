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
		public PlayerView()
		{
			InitializeComponent();
		}

		public IPlayerViewModel ViewModel
		{
			get { return (IPlayerViewModel)GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(IPlayerViewModel), typeof(PlayerView), new PropertyMetadata(null));

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (IPlayerViewModel)value; }
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.DataContext = this.ViewModel;
		}
	}
}
