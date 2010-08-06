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
                rtn.Add(new Mp3Info {
                    Name = file,
                    FileName = file,
                    FullPath = Path.Combine( path, file )
                });
            }
            foreach( var folder in directories )
            {
                rtn = rtn.Concat(
                    GetSongs(Path.Combine(path, folder))
                    ).ToList();
            }
            return rtn;
        }
    }
}
