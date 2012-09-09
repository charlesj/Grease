// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease
{
	using System;
	using System.Globalization;
	using System.Threading;
	using System.Windows;
	using System.Windows.Forms;
	using System.Windows.Input;
	using System.Windows.Media.Imaging;

	using Grease.Core;

	using MahApps.Metro;

	using Label = System.Windows.Controls.Label;

	/// <summary>
	/// 	Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// The _engine.
		/// </summary>
		private readonly IMusicEngine engine;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		public MainWindow()
		{
			this.InitializeComponent();

			ThemeManager.ChangeTheme(
				this, new Accent("GreaseTheme", new Uri("pack://application:,,,/Grease;component/GreaseTheme.xaml")), Theme.Light);

			// keyboard shortcuts
			KeyCommands.PlayPauseCommand.InputGestures.Add(new KeyGesture(Key.Space));
			KeyCommands.NextTrackCommand.InputGestures.Add(new KeyGesture(Key.Right));
			KeyCommands.PreviousTrackCommand.InputGestures.Add(new KeyGesture(Key.Left));
			KeyCommands.VolumeDownCommand.InputGestures.Add(new KeyGesture(Key.Down));
			KeyCommands.VolumeUpCommand.InputGestures.Add(new KeyGesture(Key.Up));

			this.volumeSlider.Value = 1.00;
			this.RefreshVolumeValueDisplay();
			this.engine =
				new MusicFileEngine(
					new WpfPlayer(this.Player, this.sliderPosition, this.lblElapsed, this.lblRemaining), 
					new WindowsFileSystemAccess(new TagLibInformationProvider()));

			var md = Settings.Default.MusicDirectory;
			if (!string.IsNullOrEmpty(md) && md != "None")
			{
				this.LoadSongs(md);
			}
		}

		/// <summary>
		/// The btn choose directory click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void BtnChooseDirectoryClick(object sender, RoutedEventArgs e)
		{
			var pathFinder = new FolderBrowserDialog();
			if (!string.IsNullOrEmpty(Settings.Default.MusicDirectory))
			{
				pathFinder.SelectedPath = Settings.Default.MusicDirectory;
			}

			pathFinder.ShowDialog();
			if (!string.IsNullOrEmpty(pathFinder.SelectedPath))
			{
				this.LoadSongs(pathFinder.SelectedPath);
				Settings.Default.MusicDirectory = pathFinder.SelectedPath;
				Settings.Default.Save();
			}
		}

		/// <summary>
		/// The btn next click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void BtnNextClick(object sender, RoutedEventArgs e)
		{
			this.Play(true);
		}

		/// <summary>
		/// The btn pause click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void BtnPauseClick(object sender, RoutedEventArgs e)
		{
			this.Pause();
		}

		/// <summary>
		/// The btn play click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void BtnPlayClick(object sender, RoutedEventArgs e)
		{
			this.Play();
		}

		/// <summary>
		/// The btn previous click.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void BtnPreviousClick(object sender, RoutedEventArgs e)
		{
			this.Previous();
		}

		/// <summary>
		/// The change media volume.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			if (this.engine != null)
			{
				this.engine.ChangeVolume(this.volumeSlider.Value);
			}

			this.RefreshVolumeValueDisplay();
		}

		/// <summary>
		/// The completed song.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void CompletedSong(object sender, RoutedEventArgs e)
		{
			this.Play(true);
		}

		/// <summary>
		/// The load songs.
		/// </summary>
		/// <param name="folder">
		/// The folder.
		/// </param>
		private void LoadSongs(string folder)
		{
			var songLoader = new Thread(this.LoadSongsInBackgound) { Name = "Background Song Loader" };
			songLoader.Start(folder);
			this.lblStatus.Content = "Loading Music...";
		}

		/// <summary>
		/// The load songs in backgound.
		/// </summary>
		/// <param name="folderPath">
		/// The folder path.
		/// </param>
		private void LoadSongsInBackgound(object folderPath)
		{
			this.engine.Load((string)folderPath);
			this.lblStatus.Dispatcher.Invoke(
				new Action(delegate { this.lblStatus.Content = this.engine.FoundCount + " files found"; }));

			// gdMusic.Dispatcher.Invoke(new Action(delegate { gdMusic}));
		}

		/// <summary>
		/// The media opened.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void MediaOpened(object sender, RoutedEventArgs e)
		{
			// currTimeline.Source = Player.Source;
			// currTimeline.1

			// sliderPosition.Maximum = Player.NaturalDuration.TimeSpan.Milliseconds;
			// lblRemaining.Content = "M" + Player.NaturalDuration.TimeSpan.Milliseconds;
		}

		/// <summary>
		/// The next track executed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void NextTrackExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.Play(true);
		}

		/// <summary>
		/// The on curr timeline on current time invalidated.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="args">
		/// The args.
		/// </param>
		// ReSharper disable UnusedMember.Local
		// ReSharper disable UnusedParameter.Local
		private void OnCurrTimelineOnCurrentTimeInvalidated(object sender, EventArgs args)
		// ReSharper restore UnusedParameter.Local
		// ReSharper restore UnusedMember.Local
		{
			this.sliderPosition.Value = this.Player.Position.Milliseconds;
		}

		/// <summary>
		/// The pause.
		/// </summary>
		private void Pause()
		{
			this.engine.Pause();
		}

		/// <summary>
		/// The play.
		/// </summary>
		/// <param name="next">
		/// The next.
		/// </param>
		private void Play(bool next = false)
		{
			this.engine.Play(next);
			this.RefreshCurrentInfo();
		}

		/// <summary>
		/// The play pause executed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void PlayPauseExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (this.engine.CurrentlyPlaying)
			{
				this.Pause();
			}
			else
			{
				this.Play();
			}
		}

		/// <summary>
		/// The previous.
		/// </summary>
		private void Previous()
		{
			this.engine.Previous();
			this.RefreshCurrentInfo();
		}

		/// <summary>
		/// The previous track executed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void PreviousTrackExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			this.Previous();
		}

		/// <summary>
		/// The refresh current info.
		/// </summary>
		private void RefreshCurrentInfo()
		{
			var curr = this.engine.Current;
			this.lblCurrentlyPlaying.Content = curr.Name;
			this.lblCurrentAlbum.Content = curr.Album;
			this.lblCurrentArtist.Content = curr.Artist;
			if (curr.HasImage)
			{
				var img = new BitmapImage();
				img.BeginInit();
				img.UriSource = new Uri(curr.ImagePath);
				img.EndInit();

				// lblCurrentAlbum.Content = string.Format("h: {0} w: {1}", img.Height, img.Width);
				this.imgAlbumArt.Source = img;
			}
			else
			{
				this.imgAlbumArt.Source = null;
			}
		}

		/// <summary>
		/// The refresh volume value display.
		/// </summary>
		private void RefreshVolumeValueDisplay()
		{
			var vol = this.volumeSlider.Value;
			if (this.lblVolumeLevel == null)
			{
				this.lblVolumeLevel = new Label();
			}

			this.lblVolumeLevel.Content = (Math.Round(vol, 2) * 100).ToString(CultureInfo.InvariantCulture) + "%";
		}

		/// <summary>
		/// The volume down executed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void VolumeDownExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (Math.Abs(this.volumeSlider.Value - 0) > 0.1 && this.volumeSlider.Value > .1)
			{
				this.volumeSlider.Value = this.volumeSlider.Value - .1;
			}
			else
			{
				this.volumeSlider.Value = 0;
			}
		}

		/// <summary>
		/// The volume up executed.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void VolumeUpExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (Math.Abs(this.volumeSlider.Value - 1) > 0.1 && this.volumeSlider.Value < .9)
			{
				this.volumeSlider.Value = this.volumeSlider.Value + .1;
			}
			else
			{
				this.volumeSlider.Value = 1;
			}
		}

		/// <summary>
		/// The windows command got focus.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void WindowsCommandGotFocus(object sender, RoutedEventArgs e)
		{
			Keyboard.ClearFocus();
		}

		/// <summary>
		/// The windows command got keyboard focus.
		/// </summary>
		/// <param name="sender">
		/// The sender.
		/// </param>
		/// <param name="e">
		/// The e.
		/// </param>
		private void WindowsCommandGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Keyboard.ClearFocus();
		}

		// private void PlayerMediaOpened(object sender, RoutedEventArgs ee)
		// {
		// var tl = new MediaTimeline(Player.Source);

		// Player.Clock = tl.CreateClock(true) as MediaClock;

		// Player.MediaOpened += (o, e) =>
		// {
		// sliderPosition.Maximum = Player.NaturalDuration.TimeSpan.Seconds;
		// Player.Clock.Controller.Pause();
		// };

		// sliderPosition.ValueChanged += (o, e) =>
		// {
		// Player.Clock.Controller.Seek(TimeSpan.FromSeconds(sliderPosition.Value), TimeSeekOrigin.BeginTime);
		// };
		// }
	}
}