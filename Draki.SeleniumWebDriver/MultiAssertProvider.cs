using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Draki.Interfaces;

namespace Draki
{
    public class MultiAssertProvider : IAssertProvider
    {
        private readonly CommandProviderList commandProviders = null;
        private readonly List<KeyValuePair<IAssertProvider, ICommandProvider>> providers = null;

        private IAssertProvider BuildAssertProvider(ICommandProvider commandProvider)
        {
            var assertProvider = new AssertProvider(commandProvider);

            if (ThrowExceptions)
                return assertProvider.EnableExceptions();

            return assertProvider;
        }

        private IAssertProvider BuildAssertProvider(IAssertProvider assertProvider)
        {
            if (ThrowExceptions)
                return assertProvider.EnableExceptions();

            return assertProvider;
        }

        public MultiAssertProvider(CommandProviderList commandProviders)
        {
            this.commandProviders = commandProviders; // Easier than recomposing it for EnableExceptions() call, so storing it
            providers = commandProviders.Select(x => new KeyValuePair<IAssertProvider, ICommandProvider>(new AssertProvider(x), x)).ToList();
        }

        public void Count(string selector, int count)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Count(selector, count));
        }

        public void NotCount(string selector, int count)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotCount(selector, count));
        }

        public void Count(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).Count(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void NotCount(ElementProxy element, int count)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotCount(new ElementProxy(e.Item1, e.Item2), count);
            });
        }

        public void CssClass(string selector, string className)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).CssClass(selector, className));
        }

        public void NotCssClass(string selector, string className)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotCssClass(selector, className));
        }

        public void CssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).CssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }

        public void NotCssClass(ElementProxy element, string className)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotCssClass(new ElementProxy(e.Item1, e.Item2), className);
            });
        }

        public void Text(string selector, string text)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Text(selector, text));
        }

        public void NotText(string selector, string text)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotText(selector, text));
        }

        public void Text(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void NotText(ElementProxy element, string text)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), text);
            });
        }

        public void Text(string selector, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Text(selector, matchFunc));
        }

        public void NotText(string selector, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotText(selector, matchFunc));
        }

        public void Text(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).Text(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotText(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotText(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Value(string selector, string value)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Value(selector, value));
        }

        public void NotValue(string selector, string value)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotValue(selector, value));
        }

        public void Value(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void NotValue(ElementProxy element, string value)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), value);
            });
        }

        public void Value(string selector, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Value(selector, matchFunc));
        }

        public void NotValue(string selector, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotValue(selector, matchFunc));
        }

        public void Value(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).Value(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void NotValue(ElementProxy element, Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(element.Elements, e =>
            {
                BuildAssertProvider(e.Item1).NotValue(new ElementProxy(e.Item1, e.Item2), matchFunc);
            });
        }

        public void Url(Uri expectedUrl)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Url(expectedUrl));
        }
        public void NotUrl(Uri expectedUrl)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotUrl(expectedUrl));
        }

        public void Url(Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Url(urlExpression));
        }

        public void NotUrl(Expression<Func<Uri, bool>> urlExpression)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotUrl(urlExpression));
        }

        public void True(Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).True(matchFunc));
        }

        public void False(Expression<Func<bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).False(matchFunc));
        }

        public void Throws(Expression<Action> matchAction)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Throws(matchAction));
        }

        public void NotThrows(Expression<Action> matchAction)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotThrows(matchAction));
        }

        public void Exists(string selector)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Exists(selector));
        }

        public void NotExists(string selector)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotExists(selector));
        }

        public void Exists(ElementProxy element)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Exists(element));
        }

        public void NotExists(ElementProxy element)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotExists(element));
        }

        public void AlertText(string text)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).AlertText(text));
        }

        public void AlertNotText(string text)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).AlertNotText(text));
        }

        public void AlertText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).AlertText(matchFunc));
        }

        public void AlertNotText(Expression<Func<string, bool>> matchFunc)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).AlertNotText(matchFunc));
        }

        public void Visible(string selector)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Visible(selector));
        }

        public void NotVisible(string selector)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotVisible(selector));
        }

        public void Visible(ElementProxy element)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Visible(element));
        }

        public void NotVisible(ElementProxy element)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotVisible(element));
        }

        public void CssProperty(string selector, string propertyName, string propertyValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).CssProperty(selector, propertyName, propertyValue));
        }

        public void NotCssProperty(string selector, string propertyName, string propertyValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotCssProperty(selector, propertyName, propertyValue));
        }

        public void CssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).CssProperty(element, propertyName, propertyValue));
        }

        public void NotCssProperty(ElementProxy element, string propertyName, string propertyValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotCssProperty(element, propertyName, propertyValue));
        }

        public void Attribute(string selector, string attributeName, string attributeValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Attribute(selector, attributeName, attributeValue));
        }

        public void NotAttribute(string selector, string attributeName, string attributeValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotAttribute(selector, attributeName, attributeValue));
        }

        public void Attribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).Attribute(element, attributeName, attributeValue));
        }

        public void NotAttribute(ElementProxy element, string attributeName, string attributeValue)
        {
            Parallel.ForEach(providers, x => BuildAssertProvider(x.Key).NotAttribute(element, attributeName, attributeValue));
        }

        public bool ThrowExceptions { get; set; }

        public IAssertProvider EnableExceptions()
        {
            var provider = new MultiAssertProvider(commandProviders);
            provider.ThrowExceptions = true;

            return provider;
        }
    }
}
