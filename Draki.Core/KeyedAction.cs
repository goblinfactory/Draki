using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Draki
{
    public class KeyedAction
    {
        public object Key { get; }
        public Expression<Action> Action { get; }
        public KeyedAction(object key, Expression<Action> action)
        {
            Key = key;
            Action = action;
        }
    }
}
