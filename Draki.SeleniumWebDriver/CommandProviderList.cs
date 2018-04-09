using Draki.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draki.Interfaces
{
    public class CommandProviderList : List<ICommandProvider>
    {
        public CommandProviderList(IEnumerable<ICommandProvider> collection)
            :base(collection)
        {
        }
    }
}
