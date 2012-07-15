namespace Grease.Core
{
    public interface IMusicPlayer
    {
        void Play();
        void Pause();
        void ChangeVolume(double toChange);
        string Source { get; set; }
    }
}