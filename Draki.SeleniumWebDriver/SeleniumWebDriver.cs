using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Draki.Exceptions;
using Draki.Interfaces;
using Draki.Wrappers;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace Draki
{
    /// <summary>
    /// Selenium WebDriver Draki Provider
    /// </summary>
    public class SeleniumWebDriver
    {
        /// <summary>
        /// Supported browsers for the Draki Selenium provider.
        /// </summary>
        public enum Browser
        {
            /// <summary>
            /// Internet Explorer. Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer = 1,

            /// <summary>
            /// Internet Explorer (64-bit). Before using, make sure to set ProtectedMode settings to be the same for all zones.
            /// </summary>
            InternetExplorer64 = 2,

            /// <summary>
            /// Mozilla Firefox
            /// </summary>
            Firefox = 3,

            /// <summary>
            /// Google Chrome
            /// </summary>
            Chrome = 4,

            /// <summary>
            /// Safari - Experimental - Only usable with a Remote URI
            /// </summary>
            Safari = 6,

            /// <summary>
            /// iPad - Experimental - Only usable with a Remote URI
            /// </summary>
            iPad = 7,

            /// <summary>
            /// iPhone - Experimental - Only usable with a Remote URI
            /// </summary>
            iPhone = 8,

            /// <summary>
            /// Android - Experimental - Only usable with a Remote URI
            /// </summary>
            Android = 9
        }

        private static TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

        /// <summary>
        /// Bootstrap Selenium provider and utilize Firefox.
        /// </summary>
        public static void Bootstrap()
        {
            Bootstrap(Browser.Firefox);
        }
        
        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser"></param>
        public static void Bootstrap(Browser browser)
        {
            Bootstrap(browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider and utilize the specified <paramref name="browser"/>.
        /// </summary>
        /// <param name="browser"></param>
        public static void Bootstrap(Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                var browserDriver = GenerateBrowserSpecificDriver(browser, commandTimeout);
                container.Register<IWebDriver>((c, o) => browserDriver());
            };
        }
        
        public static void Bootstrap(params Browser[] browsers)
        {
            Bootstrap(DefaultCommandTimeout, browsers);
        }

        public static void Bootstrap(TimeSpan commandTimeout, params Browser[] browsers)
        {
            if (browsers.Length == 1)
            {
                Bootstrap(browsers.First());
                return;
            }

            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                FluentTest.IsMultiBrowserTest = true;

                var webDrivers = new List<Func<IWebDriver>>();
                browsers.Distinct().ToList().ForEach(x => webDrivers.Add(GenerateBrowserSpecificDriver(x, commandTimeout)));

                var commandProviders = new CommandProviderList(webDrivers.Select(x => new CommandProvider(x, new LocalFileStoreProvider())));
                container.Register<CommandProviderList>(commandProviders);

                container.Register<ICommandProvider, MultiCommandProvider>();
                container.Register<IAssertProvider, MultiAssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();
            };
        }
        
        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="browser"></param>
        public static void Bootstrap(Uri driverUri, Browser browser)
        {
            Bootstrap(driverUri, browser, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver targeting the requested browser
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="browser"></param>
        /// <param name="commandTimeout"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                ICapabilities browserCapabilities = GenerateDesiredCapabilities(browser);
                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }

        
        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities)
        {
            Bootstrap(driverUri, browser, capabilities, DefaultCommandTimeout);
        }

        /// <summary>
        /// Bootstrap Selenium provider using a Remote web driver service with the requested capabilities
        /// </summary>
        /// <param name="driverUri"></param>
        /// <param name="capabilities"></param>
        /// <param name="commandTimeout"></param>
        public static void Bootstrap(Uri driverUri, Browser browser, Dictionary<string, object> capabilities, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                ICapabilities browserCapabilities = GenerateDesiredCapabilities(browser, capabilities);

                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }
        
        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities)
        {
            Bootstrap(driverUri, capabilities, DefaultCommandTimeout);
        }

        public static void Bootstrap(Uri driverUri, Dictionary<string, object> capabilities, TimeSpan commandTimeout)
        {
            FluentSettings.Current.ContainerRegistration = (container) =>
            {
                container.Register<ICommandProvider, CommandProvider>();
                container.Register<IAssertProvider, AssertProvider>();
                container.Register<IFileStoreProvider, LocalFileStoreProvider>();

                DesiredCapabilities browserCapabilities = new DesiredCapabilities(capabilities);
                container.Register<IWebDriver, RemoteWebDriver>(new EnhancedRemoteWebDriver(driverUri, browserCapabilities, commandTimeout));
            };
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser)
        {
            return GenerateBrowserSpecificDriver(browser, DefaultCommandTimeout);
        }

        private static Func<IWebDriver> GenerateBrowserSpecificDriver(Browser browser, TimeSpan commandTimeout)
        {
            string driverPath = string.Empty;
            switch (browser)
            {
                case Browser.InternetExplorer:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer32.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout));
                case Browser.InternetExplorer64:
                    driverPath = EmbeddedResources.UnpackFromAssembly("IEDriverServer64.exe", "IEDriverServer.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));
                    return new Func<IWebDriver>(() => new Wrappers.IEDriverWrapper(Path.GetDirectoryName(driverPath), commandTimeout));
                case Browser.Firefox:
                    return new Func<IWebDriver>(() => {
                        var service = FirefoxDriverService.CreateDefaultService();
                        var profile = new FirefoxProfile
                        {
                            EnableNativeEvents = false,
                            AcceptUntrustedCertificates = true,
                            AlwaysLoadNoFocusLibrary = true
                        };
                        var ffOptions = new FirefoxOptions()
                        {
                            Profile = profile,
                        };
                        return new FirefoxDriver(service, ffOptions, commandTimeout);
                    });
                case Browser.Chrome:
                    driverPath = EmbeddedResources.UnpackFromAssembly("chromedriver.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                    var chromeService = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath));
                    chromeService.SuppressInitialDiagnosticInformation = true;

                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--log-level=3");

                    return new Func<IWebDriver>(() => new ChromeDriver(chromeService, chromeOptions, commandTimeout));
            }

            throw new NotImplementedException("Selected browser " + browser.ToString() + " is not supported yet.");
        }

        private static ICapabilities GenerateDesiredCapabilities(Browser browser, Dictionary<string, object> desiredbrowsercaps = null)
        {
            Dictionary<string, object> browsercaps = desiredbrowsercaps ?? new Dictionary<string, object>();
            ICapabilities CreateChromeOptions(string emulateDevice) {
                var options = new ChromeOptions();
                options.EnableMobileEmulation("iPad");
                var caps = CreateOptions(options, browsercaps);
                return caps;
            }

            ICapabilities CreateOptions(DriverOptions options, Dictionary<string, object> capabilities)
            {
                options.AddAdditionalCapability("javascriptEnabled", true);
                foreach(var cap in capabilities)
                {
                    options.AddAdditionalCapability(cap.Key, cap.Value);
                }
                var caps = options.ToCapabilities();
                return caps;
            }


            ICapabilities browserCapabilities;

            switch (browser)
            {
                case Browser.InternetExplorer:
                case Browser.InternetExplorer64:
                    browserCapabilities = CreateOptions(new InternetExplorerOptions(), browsercaps);
                    break;
                case Browser.Firefox:
                    browserCapabilities = CreateOptions(new FirefoxOptions(), browsercaps);
                    break;
                case Browser.iPhone:
                    browserCapabilities = CreateChromeOptions("iPhone");
                    break;
                case Browser.iPad:
                    browserCapabilities = CreateChromeOptions("iPad");
                    break;
                case Browser.Chrome:
                    browserCapabilities = CreateOptions(new ChromeOptions(), browsercaps);
                    break;
                case Browser.Safari:
                    browserCapabilities = CreateOptions(new SafariOptions(), browsercaps);
                    break;
                default:
                    throw new FluentException("Selected browser [{0}] not supported. Unable to determine appropriate capabilities.", browser.ToString());
            }
            // see https://github.com/SeleniumHQ/selenium/wiki/DesiredCapabilities
            return browserCapabilities;
        }


    }
}
