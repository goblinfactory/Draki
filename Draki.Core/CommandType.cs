using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draki
{
    public enum CommandType
    {
        Action,
        Expect,
        Assert,
        Wait,
        NoRetry
    }
}
