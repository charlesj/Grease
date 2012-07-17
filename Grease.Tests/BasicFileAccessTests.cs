using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Grease.Core;
using Xunit;

namespace Grease.Tests
{
    public class BasicFileAccessTests
    {
        [Fact]
        public void RecursiveFunctionWorks()
        {
            var fsa = new WindowsFileSystemAccess(new TagLibInformationProvider());
            var musicFileInfos = fsa.GetMusicFiles(@"E:\Dropbox\Music\Classical\Ultimate Chopin");
            Assert.True(musicFileInfos.Count > 0);
        }

        [Fact]
        public void GetFastFileAccessTiming()
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            var fsa = new WindowsFileSystemAccess(new TagLibInformationProvider());
            stopwatch.Start();
            var musicFiles = fsa.GetMusicFiles(@"E:\Dropbox\Music");
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;
            Console.WriteLine("{0} seconds {1} milliseconds {2} files found", elapsed.Seconds, elapsed.Milliseconds, musicFiles.Count);
            Assert.True(elapsed < new TimeSpan(0,0,1));
        }
    }
}
