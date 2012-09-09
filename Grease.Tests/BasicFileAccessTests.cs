// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BasicFileAccessTests.cs" company="Developing Enterprises">
//   Josh Charles
// </copyright>
// <summary>
//   Defines the BasicFileAccessTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Grease.Tests
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	using Xunit;

	[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
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
	        Assert.True(elapsed < new TimeSpan(0, 0, 1));
        }
    }
}
