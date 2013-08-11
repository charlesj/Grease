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
	public partial class PlayerView : IPlayerView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerView"/> class.
		/// </summary>
		public PlayerView()
		{
			InitializeComponent();
		}

		public IPlayerViewModel ViewModel
		{
			get { return (IPlayerViewModel)this.DataContext; }
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
				this.ViewModel = (IPlayerViewModel)value;
			}
		}

		/////// <summary>
		/////// The on apply template.
		/////// </summary>
		////public override void OnApplyTemplate()
		////{
		////	base.OnApplyTemplate();
		////	this.DataContext = this.ViewModel;
		////}
	}

	/// <summary>
	/// The PlayerView interface.
	/// </summary>
	public interface IPlayerView : IViewFor<IPlayerViewModel>
	{
		
	}
}
