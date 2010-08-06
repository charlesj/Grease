using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();
            library = new MusicLibrary();

        }

        private void btnChooseDirectory_Click(object sender, RoutedEventArgs e)
        {
            var pathFinder = new FolderBrowserDialog();
            pathFinder.RootFolder = Environment.SpecialFolder.MyDocuments;
            var path = pathFinder.ShowDialog();
            library.Songs = FolderHelper.GetSongs(pathFinder.SelectedPath);
            lblSongCount.Content = "Found " + library.Songs.Count.ToString() + " mp3's";
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (currSong == null)
            {
                Player.Source = new Uri(library.GetRandomMp3().FullPath);
                currSong = library.GetRandomMp3();
                lblCurrentlyPlaying.Content = currSong.Name;
            }
            Player.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
            currSong = library.GetRandomMp3();
            Player.Source = new Uri(currSong.FullPath);
            lblCurrentlyPlaying.Content = currSong.Name;
            Player.Play();
        }

        private void CompletedSong(object sender, RoutedEventArgs e)
        {
            btnNext_Click(sender, e);
        }
    }
}
