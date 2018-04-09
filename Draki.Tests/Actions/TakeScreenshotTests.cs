using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using System.Globalization;
using Draki.Exceptions;

namespace Draki.Tests.Actions
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
        }

        [Test]
        public void TakeScreenshot()
        {
            TextPage.Go();

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
        
        [Test]
        [Category(Category.SLOW)]
        public void ScreenshotOnFailedAction()
        {
            TextPage.Go();

            var c = Config.Settings.ScreenshotOnFailedAction;
            try
            {
                Config.ScreenshotOnFailedAction(true);

                var lastFile = MostRecentTempFile()?.Name ?? "xx.xx";

                var exception = Assert.Throws<FluentException> (() => I.Click("#nope"));

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

        [Test]
        [Category(Category.SLOW)]
        public void ScreenshotOnFailedAssert()
        {
            TextPage.Go();

            var c = Config.Settings.ScreenshotOnFailedAssert;
            Config.ScreenshotOnFailedAssert(true);
            try
            {
                var lastFile = MostRecentTempFile()?.Name ?? "xx.xx";

                Assert.Throws<FluentException>(() => I.Assert.True(() => false));

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
    }
}
