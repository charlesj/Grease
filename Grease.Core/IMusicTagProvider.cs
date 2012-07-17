using System;

namespace Grease.Core
{
    public interface IMusicTagProvider
    {
        IMusicFileInfo GetInfo(string path);
    }
}