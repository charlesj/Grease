using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Threading;
using Grease.Utils;


namespace Grease
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MusicLibrary _library;
        private Mp3Info _currSong;
        private bool _isPlaying;

        public MainWindow()
        {
            _isPlaying = false;
            InitializeComponent();

            //keyboard shortcuts
            KeyCommands.PlayPauseCommand.InputGestures.Add(new KeyGesture ( Key.Space ));
            KeyCommands.NextTrackCommand.InputGestures.Add(new KeyGesture(Key.Right));
            KeyCommands.PreviousTrackCommand.InputGestures.Add(new KeyGesture(Key.Left));
            KeyCommands.VolumeDownCommand.InputGestures.Add(new KeyGesture(Key.Down));
            KeyCommands.VolumeUpCommand.InputGestures.Add(new KeyGesture(Key.Up));
            
            _library = new MusicLibrary();
            volumeSlider.Value = 1.00;
            RefreshVolumeValueDisplay();
            var md = Settings.Default.MusicDirectory;
            if (!string.IsNullOrEmpty(md) && md != "None")
            {
                LoadSongs(md);
            }
        }

        private void LoadSongs(string folder)
        {
            var songLoader = new Thread(LoadSongsInBG) {Name = "Background Song Loader"};
            songLoader.Start(folder);
            lblSongCount.Content = "Loading Music...";
        }

        private void LoadSongsInBG(object folderPath)
        {
            _library = new MusicLibrary {Songs = FolderHelper.GetSongs((string) folderPath)};
            lblSongCount.Dispatcher.Invoke(new Action(delegate { lblSongCount.Content = _library.Songs.Count + " files found"; }));
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
            if (_isPlaying)
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
            Player.Volume = volumeSlider.Value;
            RefreshVolumeValueDisplay();
        }


        private void Play(bool next = false)
        {
            Pause();
            if (_library.Songs != null && _library.Songs.Count > 0)
            {
                if (_currSong == null || next)
                {
                    _currSong = _library.Next();
                    Player.Source = new Uri(_currSong.FullPath);
                    lblCurrentlyPlaying.Content = _currSong.Name;
                }
                Player.Play();
                _isPlaying = true;
            }
        }

        private void Pause()
        {
            Player.Pause();
            _isPlaying = false;
        }

        private void Previous()
        {
            if (_library.PlayedSongs.Count > 0)
            {
                Pause();
                _currSong = _library.Previous();
                Player.Source = new Uri(_currSong.FullPath);
                lblCurrentlyPlaying.Content = _currSong.Name;
                Player.Play();
                _isPlaying = true;
            }
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
    }
}
