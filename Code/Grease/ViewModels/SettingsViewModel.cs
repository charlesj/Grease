// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SettingsViewModel.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the SettingsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.ViewModels
{
	using System;
	using System.Windows.Forms;

	using Grease.Core;

	using ReactiveUI;

	/// <summary>
	/// The settings view model.
	/// </summary>
	public class SettingsViewModel : BaseViewModel, ISettingsViewModel
	{
		/// <summary>
		/// The settings.
		/// </summary>
		private readonly ISettings settings;

		/// <summary>
		/// The root path.
		/// </summary>
		private string rootPath;

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
		/// </summary>
		/// <param name="hostScreen">
		/// The host screen.
		/// </param>
		/// <param name="settings">The settings for the application</param>
		public SettingsViewModel(IScreen hostScreen, ISettings settings)
			: base(hostScreen)
		{
			this.settings = settings;
			this.RootPath = this.settings.RootPath;

			this.ChangeDirectoryCommand = new ReactiveCommand();
			this.ChangeDirectoryCommand.Subscribe(_ => this.ChangeDirectory());
		}

		/// <summary>
		/// Gets the url path segment.
		/// </summary>
		public override string UrlPathSegment
		{
			get
			{
				return "Settings";
			}
		}

		/// <summary>
		/// Gets or sets the root path.
		/// </summary>
		public string RootPath
		{
			get
			{
				return this.rootPath;
			}

			set
			{
				this.RaiseAndSetIfChanged(ref this.rootPath, value);
			}
		}

		/// <summary>
		/// Gets the change directory command.
		/// </summary>
		public IReactiveCommand ChangeDirectoryCommand { get; private set; }

		/// <summary>
		/// Gets the go back command.
		/// </summary>
		public IReactiveCommand GoBackCommand
		{
			get
			{
				return this.HostScreen.Router.NavigateBack;
			}
		}

		/// <summary>
		/// The change directory.
		/// </summary>
		private void ChangeDirectory()
		{
			var dialog = new FolderBrowserDialog();
			if (!string.IsNullOrEmpty(this.RootPath))
			{
				dialog.SelectedPath = this.RootPath;
			}

			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK)
			{
				this.RootPath = dialog.SelectedPath;
				this.settings.RootPath = dialog.SelectedPath;
			}
		}
	}
}
