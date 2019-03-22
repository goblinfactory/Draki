using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Draki.Tests
{
    [SetUpFixture]
    public class WebsiteRunner
    {
        private const string CONTROL_C = "\x3";
        private const string TEST_WEBSITE = "FluentAutomation.TestApplication";
        private string _wwwDir;
        Process kestrel;

        private string GetWebsiteDir([CallerFilePath] string path = "")
        {
            var slnRoot = new FileInfo(path).Directory.Parent.Parent;
            var websiteRoot = Path.Combine(slnRoot.FullName, TEST_WEBSITE);
            return websiteRoot;
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            throw new NotImplementedException("this will not work until the TestApplication website has been migrated to ASP.NET Core MVC.");
            _wwwDir = GetWebsiteDir();
            ProcessStartInfo info = new ProcessStartInfo("dotnet", "run");
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.WorkingDirectory = _wwwDir;
            kestrel = Process.Start(info);
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            kestrel.StandardInput.WriteLine(CONTROL_C);
            kestrel.Kill();
        }
    }
}
