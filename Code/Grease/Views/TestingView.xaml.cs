// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestingView.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Interaction logic for TestingView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Views
{
	using System.Diagnostics;

	using ReactiveUI;

	/// <summary>
	/// Interaction logic for TestingView.xaml
	/// </summary>
	public partial class TestingView : ITestingView
	{
		public TestingView()
		{
			InitializeComponent();
		}

		public ITestingViewModel ViewModel
		{
			get { return (ITestingViewModel)this.DataContext; }
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
				this.ViewModel = (ITestingViewModel)value;
			}
		}
	}

	public interface ITestingView : IViewFor<ITestingViewModel>
	{
		
	}

	public interface ITestingViewModel : IRoutableViewModel
	{
		string Name { get; set; }
	}

	public class TestingViewModel : ReactiveObject, ITestingViewModel
	{
		public TestingViewModel(IScreen screen)
		{
			this.HostScreen = screen;
		}
		public string Name { get; set; }

		public string UrlPathSegment { get
		{
			return "Testing";
		} }

		public IScreen HostScreen { get; private set; }
	}
}
