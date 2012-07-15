using System.Collections.Generic;

namespace Grease.Core
{
    public interface IGreaseFileSystemAccess
    {
        List<MusicFileInfo> GetMusicFiles(string path);
        List<AlbumInfo> GetAlbums(string path);
    }
}