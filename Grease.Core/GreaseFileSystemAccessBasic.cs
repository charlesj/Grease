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
            var playableExtensions = new List<string> {"*.mp3", "*.m4a"};
            var rtn = new List<MusicFileInfo>();
            var currentDirectory = new DirectoryInfo(path);
            string image = GetImage(currentDirectory);
            foreach (var extension in playableExtensions)
            {
                var musicFiles = currentDirectory.GetFiles(extension);
                rtn.AddRange(musicFiles.Select(musicfile => GetMusicFileInfo(musicfile, image)));
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

        private string GetImage(DirectoryInfo directory)
        {
            var imageExtensions = new List<string> { "*.jpg", "*.png" };
            var candidates = new List<FileInfo>();
            foreach (var extension in imageExtensions)
            {
                var imageFiles = directory.GetFiles(extension);
                candidates.AddRange(imageFiles);
            }
            if (candidates.Count > 0)
            {
                var largest = candidates.OrderByDescending(fi => fi.Length).First();
                return largest.FullName;
            }
            return string.Empty;
        }

        private MusicFileInfo GetMusicFileInfo(FileInfo info, string imagePath)
        {
            var taglib = TagLib.File.Create(info.FullName);
            var rtn= new MusicFileInfo
                          {
                              FullPath = info.FullName,
                              FileName = info.Name,
                              Name = taglib.Tag.Title,
                              Album = taglib.Tag.Album,
                              Artist = taglib.Tag.AlbumArtists.ToString(),
                              TrackNum = (int) taglib.Tag.Track
                          };
            if (string.IsNullOrEmpty(rtn.Name))
            {
                rtn.Name = rtn.FileName;
            }
            if (!string.IsNullOrEmpty(imagePath))
            {
                rtn.HasImage = true;
                rtn.ImagePath = imagePath;
            }
            return rtn;
        }
    }
}
