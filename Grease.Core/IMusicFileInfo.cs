namespace Grease.Core
{
    public interface IMusicFileInfo
    {
        string Name { get; set; }
        string FileName { get; set; }
        string FullPath { get; set; }
        string Album { get; set; }
        string Artist { get; set; }
        int TrackNum { get; set; }
        bool HasImage { get; set; }
        string ImagePath { get; set; }
    }
}