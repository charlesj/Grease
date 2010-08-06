using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Grease.Utils
{
    class FolderHelper
    {
        public static List<Mp3Info> GetSongs(string path)
        {
            var rtn = new List<Mp3Info>();
            string[] directories = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path, "*.mp3");
            foreach (var file in files)
            {
                rtn.Add(GetInfo(file));
            }
            foreach( var folder in directories )
            {
                rtn = rtn.Concat(
                    GetSongs(Path.Combine(path, folder))
                    ).ToList();
            }
            return rtn;
        }

        private static Mp3Info GetInfo(string file)
        {
            var mp3 = new Mp3Info();
            mp3.FullPath = file;
            mp3.FileName = file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\")-1);
            mp3.Name = mp3.FileName.Replace(".mp3", string.Empty);
            return mp3;
        }
    }
}
