using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draki
{
    public interface IFileStoreProvider
    {
        bool SaveScreenshot(FluentSettings settings, byte[] contents, string fileName);
    }
}
