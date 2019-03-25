using System;
using System.Globalization;

namespace Draki
{
    public static class KeyExtensions
    {
        public static string ToSeleniumKey(this Key key)
        {
            return Convert.ToString(Convert.ToChar((int)key, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        }
    }
}