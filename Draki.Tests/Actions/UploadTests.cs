using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Draki.Tests.Actions
{
    public class UploadTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void UploadFileTest()
        {
            InputsPage.Go();
            var path = GetPath();
            I.Enter(path).In("input[type='file']");
            I.Click("#doUpload");
            I.WaitUntil(() => I.Expect.Text("Upload results").In("h2"));
        }

        private string GetPath()
        {
            var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var fullpath = Path.Combine(fi.Directory.FullName, "Draki.Tests.dll.config");
            if (!File.Exists(fullpath)) Assert.Inconclusive($"Could not find file to upload:{fullpath}");
            return fullpath;
        }
    }
}
