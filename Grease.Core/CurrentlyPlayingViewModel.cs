using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grease.Core
{
    public class CurrentlyPlayingViewModel
    {
        public string Name { get; set; }
        public string Album { get; set; }
        public int TrackNum { get; set; }
        public string FileName { get; set; }
        public bool HasImage { get; set; }
        public string ImagePath { get; set; }
    }
}
