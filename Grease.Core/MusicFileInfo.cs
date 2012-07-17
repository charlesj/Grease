using System;

namespace Grease.Core
{
    public class MusicFileInfo : IMusicFileInfo
    {
        private bool _haveTriedToLoad;
        private IMusicTagProvider _tagProvider;

        private string _name;
        public string Name
        {
            get
            {
                LoadAdditionalInfo();  
                return _name; 
            }
            set { _name = value; }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                LoadAdditionalInfo();
                return _fileName;
            }
            set { _fileName = value; }
        }

        public string FullPath { get; set; }
        private string _album;
        public string Album
        {
            get
            {
                LoadAdditionalInfo();
                return _album;
            }
            set { _album = value; }
        }

        private string _artist;
        public string Artist
        {
            get
            {
                LoadAdditionalInfo();
                return _artist;
            }
            set { _artist = value; }
        }

        private int _trackNum;

        public MusicFileInfo(IMusicTagProvider tagProvider)
        {
            if (tagProvider != null)
            {
                _tagProvider = tagProvider;
            }
            else
            {
                throw new ArgumentNullException("tagProvider");
            }
            _haveTriedToLoad = false;
        }

        public int TrackNum
        {
            get
            {
                LoadAdditionalInfo();
                return _trackNum;
            }
            set { _trackNum = value; }
        }

        public bool HasImage { get; set; }
        public string ImagePath { get; set; }

        private void LoadAdditionalInfo()
        {
            if (!_haveTriedToLoad)
            {
                var loaded = _tagProvider.GetInfo(FullPath);
                Album = loaded.Album;
                Artist = loaded.Artist;
                Name = loaded.Name;
                FileName = loaded.FileName;
                HasImage = loaded.HasImage;
                ImagePath = loaded.ImagePath;
                TrackNum = loaded.TrackNum;
                _haveTriedToLoad = true;
            }
        }
    }
}
