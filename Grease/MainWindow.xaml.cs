using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Threading;
using Grease.Utils;


namespace Grease
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicLibrary library;
        private Mp3Info currSong;
        private bool isPlaying = false;
        private bool isShuffle = false;
        private bool isRepeat = false;

        public MainWindow()
        {
            InitializeComponent();

            //keyboard shortcuts
            KeyCommands.PlayPauseCommand.InputGestures.Add(new KeyGesture ( Key.Space ));
            KeyCommands.NextTrackCommand.InputGestures.Add(new KeyGesture(Key.Right));
            KeyCommands.PreviousTrackCommand.InputGestures.Add(new KeyGesture(Key.Left));
            KeyCommands.VolumeDownCommand.InputGestures.Add(new KeyGesture(Key.Down));
            KeyCommands.VolumeUpCommand.InputGestures.Add(new KeyGesture(Key.Up));
            
            library = new MusicLibrary();
            volumeSlider.Value = (double)1.00;
            RefreshVolumeValueDisplay();
            var md = Settings.Default.MusicDirectory;
            if (!string.IsNullOrEmpty(md) && md != "None")
            {
                LoadSongs(md);
            }
        }

        private void LoadSongs(string folder)
        {
            Thread songLoader = new Thread(LoadSongsInBG);
            songLoader.Name = "Background Song Loader";
            songLoader.Start(folder);
            lblSongCount.Content = "Loading Music...";
        }

        private void LoadSongsInBG(object folder_path)
        {
            library.Songs = FolderHelper.GetSongs((string)folder_path);
            lblSongCount.Dispatcher.Invoke(new Action(delegate() { lblSongCount.Content = library.Songs.Count + " files found"; }));
        }

        private void btnChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            var pathFinder = new FolderBrowserDialog();
            //pathFinder.RootFolder = Environment.SpecialFolder.MyDocuments;
            var path = pathFinder.ShowDialog();
            if (!string.IsNullOrEmpty(pathFinder.SelectedPath))
            {
                LoadSongs(pathFinder.SelectedPath);
                Settings.Default.MusicDirectory = pathFinder.SelectedPath;
                Settings.Default.Save();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Previous();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Play(true);
        }

        private void CompletedSong(object sender, RoutedEventArgs e)
        {
            Play(true);
        }

        private void PlayPause_Executed(object sender, ExecutedRoutedEventArgs e)
        {  
            if (isPlaying)
                Pause();
            else
                Play();
        }

        private void NextTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Play(true);
        }

        private void PreviousTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Previous();
        }

        private void VolumeDown_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (volumeSlider.Value != 0 && volumeSlider.Value > .1)
                volumeSlider.Value = volumeSlider.Value - .1;
            else
                volumeSlider.Value = 0;
        }

        private void VolumeUp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (volumeSlider.Value != 1 && volumeSlider.Value < .9)
                volumeSlider.Value = volumeSlider.Value + .1;
            else
                volumeSlider.Value = 1;
        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            Player.Volume = (double)volumeSlider.Value;
            RefreshVolumeValueDisplay();
        }


        private void Play(bool next = false)
        {
            Pause();
            if (library.Songs != null && library.Songs.Count > 0)
            {
                if (currSong == null || next)
                {
                    currSong = library.Next(isShuffle,isRepeat);
                    Player.Source = new Uri(currSong.FullPath);
                    lblCurrentlyPlaying.Content = currSong.Name;
                    //vrd
                    lblCount.Content = library.songcount;
                    //vrd

                }
                Player.Play();
                isPlaying = true;
            }
        }

        private void Pause()
        {
            Player.Pause();
            isPlaying = false;
        }

        private void Previous()
        {
            if (library.PlayedSongs.Count > 0)
            {
                Pause();
                currSong = library.Previous();
                Player.Source = new Uri(currSong.FullPath);
                lblCurrentlyPlaying.Content = currSong.Name;
                //vrd
                lblCount.Content = library.songcount;
                //
                Player.Play();
                isPlaying = true;
            }
        }

        private void RefreshVolumeValueDisplay()
        {
            var vol = volumeSlider.Value;
            if (lblVolumeLevel == null)
                lblVolumeLevel = new System.Windows.Controls.Label();
            lblVolumeLevel.Content = (Math.Round(vol, 2) * 100).ToString() + "%";
        }

        private void chkShuffle_Checked(object sender, RoutedEventArgs e)
        {
            isShuffle = true;
            library.PlayedSongs.Clear();
            library.CurrentSongIndex = -1;
        }

        private void chkShuffle_Unchecked(object sender, RoutedEventArgs e)
        {
            isShuffle = false;
        }

        private void chkRepeat_Checked(object sender, RoutedEventArgs e)
        {
            isRepeat = true;
        }

        private void chkRepeat_Unchecked(object sender, RoutedEventArgs e)
        {
            isRepeat = false;
        }
    }
}
