namespace Draki
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

}
