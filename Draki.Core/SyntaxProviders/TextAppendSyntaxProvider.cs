using Draki.Exceptions;
using Draki.Interfaces;

namespace Draki
{
    public partial class ActionSyntaxProvider
    {
        public class TextAppendSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;
            protected bool isAppend = false;

            internal TextAppendSyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextAppendSyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Append().To() is only supported on text elements (input, textarea, etc).");

                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.AppendText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.AppendTextWithoutEvents(element, text);
                }

                return this.syntaxProvider;
            }
        }

    }
}