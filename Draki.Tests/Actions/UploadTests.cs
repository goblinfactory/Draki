using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Draki.Tests.Actions
{
    public class UploadTests : BaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void UploadFileTest()
        {
            InputsPage.Go();
            var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var file = Path.Combine(fi.Directory.FullName, "Draki.Tests.dll.config");
            if (!File.Exists(file)) Assert.Inconclusive($"Could not find file to upload:{file}");
            // e.g. C:/src/git-alan-public/Draki/Draki.Tests/bin/Debug/Draki.Tests.dll.config

            // do I need to run with elevated priveledges to execute this test?
            var upload = "input[type='file']";
            I.Upload(upload, file);
            I.Expect.Text("Upload results").In("h2");
            //I.Expect.Text("File saved to").In("p.upload-results");
        }
    }
}
