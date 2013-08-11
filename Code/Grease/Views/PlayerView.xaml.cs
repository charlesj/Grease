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
	using Grease.ViewModels;

	using ReactiveUI;

	/// <summary>
	/// Interaction logic for PlayerView.xaml
	/// </summary>
	public partial class PlayerView : IPlayerView
	{
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
			get
			{
				return (IPlayerViewModel)this.DataContext;
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
				this.ViewModel = (IPlayerViewModel)value;
			}
		}
	}
}
