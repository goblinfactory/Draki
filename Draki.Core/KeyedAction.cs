using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Draki
{
    public class FindText
    {
        public object Key { get; }
        public string Selector { get; }
        public string ContainsText { get; }

        public FindText(object key, string selector, string containsText)
        {
            Key = key;
            Selector = selector;
            ContainsText = containsText;
        }
    }
}
