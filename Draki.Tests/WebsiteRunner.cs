﻿// for now we're relying on manually running the website before tests, at least until the TestApplication has
// been migrated to asp.net core mvc, so that we can use kestrel self hosting via dotnet run, then the test below
// will start and stop the website for the duration of the acceptance tests.
// --------------------------------------------------------------------------------------------------------------

//using System;
//using System.Diagnostics;
//using System.IO;
//using System.Runtime.CompilerServices;
//using Draki.Tests.Internal;
//using NUnit.Framework;

//namespace Draki.Tests
//{
//    [SetUpFixture]
//    public class WebsiteRunner
//    {
//        private const string CONTROL_C = "\x3";
//        private const string TEST_WEBSITE = "FluentAutomation.TestApplication";
//        private string _wwwDir;
//        Process kestrel;

//        private string GetWebsiteDir([CallerFilePath] string path = "")
//        {
//            var slnRoot = new FileInfo(path).Directory.Parent.Parent;
//            var websiteRoot = Path.Combine(slnRoot.FullName, TEST_WEBSITE);
//            return websiteRoot;
//        }

//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            throw new NotImplementedException("this will not work until the TestApplication website has been migrated to ASP.NET Core MVC.");
//            _wwwDir = GetWebsiteDir();
//            kestrel = ProcessRunner.StartProcess("dotnet", "run", _wwwDir);
//        }

//        [OneTimeTearDown]
//        public void OneTimeTeardown()
//        {
//            kestrel.StandardInput.WriteLine(CONTROL_C);
//            kestrel.Kill();
//        }
//    }
//}
