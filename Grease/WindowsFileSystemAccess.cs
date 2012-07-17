using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grease.Core;

namespace Grease
{
    public class WindowsFileSystemAccess : IGreaseFileSystemAccess
    {
        private readonly IMusicTagProvider _provider;

        public WindowsFileSystemAccess(IMusicTagProvider provider)
        {
            _provider = provider;
        }
        
        public List<MusicFileInfo> GetMusicFiles(string path)
        {
            var playableExtensions = new List<string> { "*.mp3", "*.m4a" };
            var rtn = new List<MusicFileInfo>();
            var directories = Directory.GetDirectories(path);
            foreach (var extension in playableExtensions)
            {
                var musicFiles = Directory.GetFiles(path, extension);
                rtn.AddRange(musicFiles.Select(musicfile => GetMusicFileInfo(musicfile)));
            }

            foreach (var directory in directories)
            {
                rtn = rtn.Concat(GetMusicFiles(directory)).ToList();
            }

            return rtn;
        }
    
        private MusicFileInfo GetMusicFileInfo(string path)
        {
            return new MusicFileInfo(_provider) {FullPath = path};
        }


        public List<AlbumInfo> GetAlbums(string path)
        {
            throw new NotImplementedException();
        }
    }
}
