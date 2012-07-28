using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Grease.Core;
using MahApps.Metro;
using Binding = System.Windows.Forms.Binding;


namespace Grease
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly IMusicEngine _engine;

		public MainWindow()
		{
			InitializeComponent();
			
			ThemeManager.ChangeTheme(this, new Accent("GreaseTheme", new Uri("pack://application:,,,/Grease;component/GreaseTheme.xaml")), Theme.Light);

			//keyboard shortcuts
			KeyCommands.PlayPauseCommand.InputGestures.Add(new KeyGesture ( Key.Space ));
			KeyCommands.NextTrackCommand.InputGestures.Add(new KeyGesture(Key.Right));
			KeyCommands.PreviousTrackCommand.InputGestures.Add(new KeyGesture(Key.Left));
			KeyCommands.VolumeDownCommand.InputGestures.Add(new KeyGesture(Key.Down));
			KeyCommands.VolumeUpCommand.InputGestures.Add(new KeyGesture(Key.Up));
			
			volumeSlider.Value = 1.00;
			RefreshVolumeValueDisplay();
			_engine = new MusicFileEngine(new WpfPlayer(Player, sliderPosition, lblElapsed, lblRemaining), new WindowsFileSystemAccess(new TagLibInformationProvider()));

			var md = Settings.Default.MusicDirectory;
			if (!string.IsNullOrEmpty(md) && md != "None")
			{
				LoadSongs(md);
			}
		}

		private void OnCurrTimelineOnCurrentTimeInvalidated(object sender, EventArgs args)
		{
			sliderPosition.Value = Player.Position.Milliseconds;
		}

		private void LoadSongs(string folder)
		{
			var songLoader = new Thread(LoadSongsInBackgound) {Name = "Background Song Loader"};
			songLoader.Start(folder);
			lblStatus.Content = "Loading Music...";
		}

		private void LoadSongsInBackgound(object folderPath)
		{
			_engine.Load((string)folderPath);
			lblStatus.Dispatcher.Invoke(new Action(delegate { lblStatus.Content = _engine.FoundCount + " files found"; }));
			//gdMusic.Dispatcher.Invoke(new Action(delegate { gdMusic}));
		}

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
				LoadSongs(pathFinder.SelectedPath);
				Settings.Default.MusicDirectory = pathFinder.SelectedPath;
				Settings.Default.Save();
			}
		}

		private void BtnPlayClick(object sender, RoutedEventArgs e)
		{
			Play();
		}

		private void BtnPreviousClick(object sender, RoutedEventArgs e)
		{
			Previous();
		}

		private void BtnPauseClick(object sender, RoutedEventArgs e)
		{
			Pause();
		}

		private void BtnNextClick(object sender, RoutedEventArgs e)
		{
			Play(true);
		}

		private void CompletedSong(object sender, RoutedEventArgs e)
		{
			Play(true);
		}

		private void PlayPauseExecuted(object sender, ExecutedRoutedEventArgs e)
		{  
			if (_engine.IsPlaying)
				Pause();
			else
				Play();
		}

		private void NextTrackExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Play(true);
		}

		private void PreviousTrackExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			Previous();
		}

		private void VolumeDownExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (Math.Abs(volumeSlider.Value - 0) > 0.1 && volumeSlider.Value > .1)
				volumeSlider.Value = volumeSlider.Value - .1;
			else
				volumeSlider.Value = 0;
		}

		private void VolumeUpExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			if (Math.Abs(volumeSlider.Value - 1) > 0.1 && volumeSlider.Value < .9)
				volumeSlider.Value = volumeSlider.Value + .1;
			else
				volumeSlider.Value = 1;
		}

		private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
		{
			if (_engine != null) _engine.ChangeVolume(volumeSlider.Value);
			RefreshVolumeValueDisplay();
		}

		private void RefreshCurrentInfo()
		{
			var curr = _engine.Current;
			lblCurrentlyPlaying.Content = curr.Name;
			lblCurrentAlbum.Content = curr.Album;
			lblCurrentArtist.Content = curr.Artist;
			if (curr.HasImage)
			{
				var img = new BitmapImage();
				img.BeginInit();
				img.UriSource = new Uri(curr.ImagePath);
				img.EndInit();
				//lblCurrentAlbum.Content = string.Format("h: {0} w: {1}", img.Height, img.Width);
				imgAlbumArt.Source = img;
			}
			else
			{
				imgAlbumArt.Source = null;
			}
			
		}

		private void Play(bool next = false)
		{
			_engine.Play(next);
			RefreshCurrentInfo();
		}

		private void Pause()
		{
			_engine.Pause();
		}

		private void Previous()
		{
			_engine.Previous();
			RefreshCurrentInfo();
		}

		private void RefreshVolumeValueDisplay()
		{
			var vol = volumeSlider.Value;
			if (lblVolumeLevel == null)
				lblVolumeLevel = new System.Windows.Controls.Label();
			lblVolumeLevel.Content = (Math.Round(vol, 2) * 100).ToString(CultureInfo.InvariantCulture) + "%";
		}

		private void WindowsCommandGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			Keyboard.ClearFocus();
		}

		private void WindowsCommandGotFocus(object sender, RoutedEventArgs e)
		{
			Keyboard.ClearFocus();
		}

		private void MediaOpened(object sender, RoutedEventArgs e)
		{
			//currTimeline.Source = Player.Source;
			//currTimeline.1

			//sliderPosition.Maximum = Player.NaturalDuration.TimeSpan.Milliseconds;
			//lblRemaining.Content = "M" + Player.NaturalDuration.TimeSpan.Milliseconds;
		}

		//private void PlayerMediaOpened(object sender, RoutedEventArgs ee)
		//{
		//    var tl = new MediaTimeline(Player.Source);

		//    Player.Clock = tl.CreateClock(true) as MediaClock;

		//    Player.MediaOpened += (o, e) =>
		//    {
		//        sliderPosition.Maximum = Player.NaturalDuration.TimeSpan.Seconds;
		//        Player.Clock.Controller.Pause();
		//    };

		//    sliderPosition.ValueChanged += (o, e) =>
		//    {
		//        Player.Clock.Controller.Seek(TimeSpan.FromSeconds(sliderPosition.Value), TimeSeekOrigin.BeginTime);
		//    };
		//}
	}
}
