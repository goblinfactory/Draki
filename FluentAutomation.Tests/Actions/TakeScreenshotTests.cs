using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xunit;
using System.Globalization;
using FluentAutomation.Exceptions;

namespace FluentAutomation.Tests.Actions
{
    // #ADH Do we need some type of test locking here for situations where tests are running in parallel or to prevent tests from running in parallel?
    // something similar to this discussion https://github.com/xunit/xunit/issues/1179  How to disable parallelism for a test collection?
    // Tests below are modifying global settings of saving screenshots to disk.
    public class TakeScreenshotTests : BaseTest
    {
        private string tempPath = null;

        public TakeScreenshotTests()
            : base()
        {
            tempPath = Path.GetTempPath();
            Config.ScreenshotPath(tempPath);

            TextPage.Go();
        }

        [Fact]
        public void TakeScreenshot()
        {
            var screenshotName = string.Format(CultureInfo.CurrentCulture, "TakeScreenshot_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";

            I.Assert.False(() => File.Exists(filepath));
            try
            {
                I.TakeScreenshot(screenshotName)
                    .Assert
                    .True(() => File.Exists(filepath))
                    .True(() => new FileInfo(filepath).Length > 0);
            }
            finally
            {
                File.Delete(filepath);
            }
        }
        
        [Fact]
        public void ScreenshotOnFailedAction()
        {

            var c = Config.Settings.ScreenshotOnFailedAction;
            try
            {
                Config.ScreenshotOnFailedAction(true);

                var lastFile = MostRecentTempFile()?.Name ?? "xx.xx";

                var exception = Record.Exception(() => I.Click("#nope"));
                
                Assert.IsType<FluentException>(exception);

                var newFile = MostRecentTempFile();

                I.Assert
                    .True(() => newFile != null)
                    .True(() => newFile.Name != lastFile)
                    .True(() => newFile.Exists)
                    .True(() => newFile.Length > 0);

                newFile.Delete();
            }
            finally
            {
                Config.ScreenshotOnFailedAction(c);
            }
        }

        private FileInfo MostRecentTempFile()
        {
            return (new DirectoryInfo(tempPath).GetFiles("*.png").OrderByDescending(f => f.CreationTime)).FirstOrDefault();
        }

        [Fact]
        public void ScreenshotOnFailedAssert()
        {
            var c = Config.Settings.ScreenshotOnFailedAssert;
            Config.ScreenshotOnFailedAssert(true);
            try
            {
                var lastFile = MostRecentTempFile()?.Name ?? "xx.xx";

                var exception = Record.Exception(() => I.Assert.True(() => false));

                Assert.IsType<FluentException>(exception);

                var newFile = MostRecentTempFile();

                I.Assert
                    .True(() => newFile != null)
                    .True(() => newFile.Name != lastFile)
                    .True(() => newFile.Exists)
                    .True(() => newFile.Length > 0);

                newFile.Delete();
            }
            finally
            {
                Config.ScreenshotOnFailedAssert(c);
            }

        }

        /*
        [Fact]
        public void ScreenshotOnFailedExpect()
        {
            var c = Config.Settings.ScreenshotOnFailedExpect;
            Config.ScreenshotOnFailedExpect(true);
            
            I.Expect.True(() => false);

            var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
            var filepath = this.tempPath + screenshotName + ".png";
            I.Assert
                .True(() => File.Exists(filepath))
                .True(() => new FileInfo(filepath).Length > 0);

            File.Delete(filepath);

            Config.ScreenshotOnFailedExpect(c);
        }
        */
    }
}
