// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;
	using System.Windows;
	using System.Windows.Input;

	using Grease.Core;

	using Ninject;

	using ReactiveUI;
	using ReactiveUI.Xaml;

	/// <summary>
	/// The application view model.
	/// </summary>
	public class MainViewModel : IMainViewModel
	{
		private readonly IScreen screen;

		public MainViewModel(IScreen screen)
		{
			this.screen = screen;
			this.GoToSettings = new ReactiveCommand();
			this.GoToSettings.Subscribe(param => this.ShowSettings());
		}

		/// <summary>
		/// The show settings.
		/// </summary>
		private void ShowSettings()
		{
			this.screen.Router.Navigate.Execute(RxApp.DependencyResolver.GetService<ISettingsViewModel>());
		}

		/// <summary>
		/// Gets the song opened command.
		/// </summary>
		public ReactiveCommand GoToSettings { get; private set; }

	}

	public interface IMainViewModel
	{
		ReactiveCommand GoToSettings { get; }
	}
}
