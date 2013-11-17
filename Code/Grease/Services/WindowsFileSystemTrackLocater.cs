// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsFileSystemTrackLocater.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.IO;
	using System.Threading.Tasks;

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
			this.GetMusicFiles(this.settings.RootPath);
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
			if (string.IsNullOrEmpty(pathToSearch))
			{
				return;
			}

			Task.Factory.StartNew(
				() =>
					{
						// Naudio 1.7 rocks my socks!  It will play all the formats now, thanks to media foundation reader!
						var playableExtensions = new List<string> { "*.mp3", "*.m4a", ".wav", "*.wma", "*.mp4" };  
						var allSongs = new List<string>();
						var directories = Directory.GetDirectories(pathToSearch);
						foreach (var extension in playableExtensions)
						{
							var musicFiles = Directory.GetFiles(pathToSearch, extension);
							allSongs.AddRange(musicFiles);
						}

						allSongs.ForEach(filePath => this.Found.Add(new TrackInfo(this.provider) { FullPath = filePath }));

						foreach (var directory in directories)
						{
							this.GetMusicFiles(directory);
						}
					});
		}

		/// <summary>
		/// The settings on on setting changed.
		/// </summary>
		/// <param name="args">
		/// The args.
		/// </param>
		private void SettingsOnOnSettingChanged(SettingChangedEventArgs args)
		{
			this.Found.Clear();
			if (args.Name == "RootPath")
			{
				this.GetMusicFiles(args.Value);
			}
		}
	}
}