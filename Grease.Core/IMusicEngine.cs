namespace Grease.Core
{
    public interface IMusicEngine
    {
        void Play(bool next);
        void Pause();
        void Next();
        void Previous();
        void ChangeVolume(double newVolume);
        void Load(string path);
        CurrentlyPlayingViewModel Current { get; }
        bool IsPlaying { get; set; }
        int FoundCount { get; }
    }
}