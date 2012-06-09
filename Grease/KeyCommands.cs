using System.Windows.Input;

namespace Grease
{
    public static class KeyCommands
    {
        public static RoutedCommand PlayPauseCommand = new RoutedCommand();
        public static RoutedCommand NextTrackCommand = new RoutedCommand();
        public static RoutedCommand PreviousTrackCommand = new RoutedCommand();
        public static RoutedCommand VolumeDownCommand = new RoutedCommand();
        public static RoutedCommand VolumeUpCommand = new RoutedCommand();
    }
}
