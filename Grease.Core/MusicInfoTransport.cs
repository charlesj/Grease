using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grease.Core
{
    public class MusicInfoTransport : IMusicFileInfo
    {
        public string Name { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public int TrackNum { get; set; }
        public bool HasImage { get; set; }
        public string ImagePath { get; set; }
    }
}
