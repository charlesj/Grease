// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsFileSystemTrackLocater.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;

	using Grease.Core;

	/// <summary>
	/// The windows file system access.
	/// </summary>
	public class WindowsFileSystemTrackLocater : ITrackLocater
	{
		/// <summary>
		/// The _provider.
		/// </summary>
		private readonly IMusicTagProvider provider;

		/// <summary>
		/// The settings.
		/// </summary>
		private readonly ISettings settings;

		/// <summary>
		/// Initializes a new instance of the <see cref="WindowsFileSystemTrackLocater"/> class.
		/// </summary>
		/// <param name="provider">
		/// The provider.
		/// </param>
		/// <param name="settings">
		/// The settings.
		/// </param>
		public WindowsFileSystemTrackLocater(IMusicTagProvider provider, ISettings settings)
		{
			this.Found = new ObservableCollection<ITrackInfo>();
			this.provider = provider;
			this.settings = settings;
			this.settings.OnSettingChanged += this.SettingsOnOnSettingChanged;
		}

		/// <summary>
		/// Gets the found.
		/// </summary>
		public ObservableCollection<ITrackInfo> Found { get; private set; }

		/// <summary>
		/// The get music files.
		/// </summary>
		/// <param name="pathToSearch">
		/// The path To Search.
		/// </param>
		private void GetMusicFiles(string pathToSearch)
		{
			var playableExtensions = new List<string> { "*.mp3", "*.m4a" };
			var allSongs = new List<string>();
			var directories = Directory.GetDirectories(pathToSearch);
			foreach (var extension in playableExtensions)
			{
				var musicFiles = Directory.GetFiles(pathToSearch, extension);
				allSongs.AddRange(musicFiles);
			}

			allSongs.ForEach(filePath =>
				{ 
					var processed = this.provider.GetInfo(filePath);
					this.Found.Add(processed);
				});

			foreach (var directory in directories)
			{
				this.GetMusicFiles(directory);
			}
		}

		/// <summary>
		/// The settings on on setting changed.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		private void SettingsOnOnSettingChanged(SettingChangedEventArgs args)
		{
			if (args.Name == "RootPath")
			{
				this.GetMusicFiles(args.Value);
			}
		}
	}
}