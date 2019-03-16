using Draki.Exceptions;
using Draki.Interfaces;

namespace Draki
{
    public partial class ActionSyntaxProvider
    {
        public class TextEntrySyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly string text = null;
            protected bool eventsEnabled = true;

            internal TextEntrySyntaxProvider(ActionSyntaxProvider syntaxProvider, string text)
            {
                this.syntaxProvider = syntaxProvider;
                this.text = text;
            }

            /// <summary>
            /// Set text entry to set value without firing key events. Faster, but may cause issues with applications
            /// that bind to the keyup/keydown/keypress events to function.
            /// </summary>
            /// <returns><c>TextEntrySyntaxProvider</c></returns>
            public TextEntrySyntaxProvider WithoutEvents()
            {
                this.eventsEnabled = false;
                return this;
            }

            /// <summary>
            /// Enter text into input or textarea element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider In(string selector)
            {
                return this.In(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Enter text into specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider In(ElementProxy element)
            {
                if (!element.Element.IsText)
                    throw new FluentException("Enter().In() is only supported on text elements (input, textarea, etc).");

                if (this.eventsEnabled)
                {
                    this.syntaxProvider.commandProvider.EnterText(element, text);
                }
                else
                {
                    this.syntaxProvider.commandProvider.EnterTextWithoutEvents(element, text);
                }

                return this.syntaxProvider;
            }

            /// <summary>
            /// Enter text into the active prompt
            /// </summary>
            /// <param name="accessor"></param>
            public IActionSyntaxProvider In(Alert accessor)
            {
                if (accessor.Field != AlertField.Input)
                {
                    this.syntaxProvider.commandProvider.AlertClick(Alert.Cancel);
                    throw new FluentException("Draki only supports entering text in text input of a prompt.");
                }

                this.syntaxProvider.commandProvider.AlertEnterText(text);
                return this.syntaxProvider;
            }
        }

    }
}