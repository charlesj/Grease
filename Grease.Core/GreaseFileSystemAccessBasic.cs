using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Grease.Core
{
    public class GreaseFileSystemAccessBasic : IGreaseFileSystemAccess
    {
        public List<MusicFileInfo> GetMusicFiles(string path)
        {
            var playableExtensions = new List<string> {".mp3", ".m4a"};
            var rtn = new List<MusicFileInfo>();
            var currentDirectory = new DirectoryInfo(path);

            foreach (var extension in playableExtensions)
            {
                var musicFiles = currentDirectory.GetFiles(extension);
                rtn.AddRange(musicFiles.Select(GetMusicFileInfo));
            }

            foreach (var directory in currentDirectory.GetDirectories())
            {
               rtn = rtn.Concat(GetMusicFiles(directory.FullName)).ToList();
            }

            return rtn;
        }

        public List<AlbumInfo> GetAlbums(string path)
        {
            throw new NotImplementedException();
        }

        private MusicFileInfo GetMusicFileInfo(FileInfo info)
        {
            var taglib = TagLib.File.Create(info.FullName);
            return new MusicFileInfo
                          {
                              FullPath = info.FullName,
                              FileName = info.Name,
                              Name = taglib.Tag.Title,
                              Album = taglib.Tag.Album,
                              Artist = taglib.Tag.AlbumArtists.ToString(),
                              TrackNum = (int) taglib.Tag.Track
                          };
        }
    }
}
