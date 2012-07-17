using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Grease.Core;

namespace Grease
{
    public class TagLibInformationProvider : IMusicTagProvider
    {
        public IMusicFileInfo GetInfo(string path)
        {
            var rtn = new MusicInfoTransport();
            var fileInfo = new FileInfo(path);
            rtn.FullPath = fileInfo.FullName;
            rtn.FileName = fileInfo.Name;
            rtn.ImagePath = GetImage(fileInfo);
            if (!string.IsNullOrEmpty(rtn.ImagePath))
                rtn.HasImage = true;
            var tags = TagLib.File.Create(path);
            rtn.Album = tags.Tag.Album;
            rtn.Artist = tags.Tag.JoinedAlbumArtists;
            rtn.Name = tags.Tag.Title;
            rtn.TrackNum = (int) tags.Tag.Track;

            if (string.IsNullOrEmpty(rtn.Name))
                rtn.Name = rtn.FileName;
            return rtn;
        }

        private string GetImage(FileInfo info)
        {
            var usableExtensions = new List<string> {"*.jpg", "*.png"};
            var candidates = new List<FileInfo>();
            foreach (var ext in usableExtensions)
            {
                if (info.Directory != null) 
                    candidates.AddRange(info.Directory.GetFiles(ext));
            }
            var final = candidates.OrderByDescending(fi => fi.Length).FirstOrDefault();
            if(final != null)
            {
                return final.FullName;
            }
            return string.Empty;
        }
    }
}
