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
            var fsa = new GreaseFileSystemAccessBasic();
            var musicFileInfos = fsa.GetMusicFiles(@"E:\Dropbox\Music\Classical\Ultimate Chopin");
            Assert.True(musicFileInfos.Count > 0);
        }
    }
}
