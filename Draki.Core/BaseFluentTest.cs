using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Draki.Interfaces;

namespace Draki
{
    public class BaseFluentTest : IDisposable
    {
        public ISyntaxProvider SyntaxProvider { get; set; }

        public void Dispose()
        {
            try
            {
                if (FluentSession.Current == null && this.SyntaxProvider != null)
                {
                    this.SyntaxProvider.Dispose();
                }

                if (FluentSettings.Current.MinimizeAllWindowsOnTestStart) Win32Magic.RestoreAllWindows();
            }
            catch { };
        }
    }
}
