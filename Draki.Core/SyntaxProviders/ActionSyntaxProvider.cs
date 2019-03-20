using System;
using System.Linq.Expressions;
using Draki.Exceptions;
using Draki.Interfaces;

namespace Draki
{
    public partial class ActionSyntaxProvider : IActionSyntaxProvider, IDisposable
    {
        internal readonly ICommandProvider commandProvider = null;
        internal readonly IAssertProvider assertProvider = null;
        internal FluentSettings settings = null;
        
        public ActionSyntaxProvider(ICommandProvider commandProvider, IAssertProvider assertProvider, FluentSettings settings)
        {
            this.commandProvider = commandProvider.WithConfig(settings);
            this.assertProvider = assertProvider;
            this.settings = settings;
        }

        public string GetTitle()
        {
            return this.commandProvider.GetTitle();
        }

        public bool QuickExists(string cssSelector)
        {
            return this.commandProvider.QuickExists(cssSelector);
        }

        internal ActionSyntaxProvider WithConfig(FluentSettings settings)
        {
            this.commandProvider.WithConfig(settings);
            this.settings = settings;
            return this;
        }

        #region Direct Execution Actions
        public IActionSyntaxProvider Open(string url)
        {
            if (url.StartsWith("/"))
                return this.Open(new Uri(url, UriKind.Relative));
            else
                return this.Open(new Uri(url, UriKind.Absolute));
        }

        public IActionSyntaxProvider Open(Uri url)
        {
            this.commandProvider.Navigate(url);
            return this;
        }

        public ElementProxy Find(string selector)
        {
            return commandProvider.Find(selector);
        }

        public ElementProxy FindMultiple(string selector)
        {
            return this.commandProvider.FindMultiple(selector);
        }


        public IActionSyntaxProvider Click(string selector)
        {
            return this.Click(this.Find(selector));
        }

        public IActionSyntaxProvider Click(ElementProxy element)
        {
            this.commandProvider.Click(element);
            return this;
        }

        public IActionSyntaxProvider Click(Alert alertAccessor)
        {
            if (alertAccessor.Field != AlertField.OKButton && alertAccessor.Field != AlertField.CancelButton)
            {
                this.Click(Alert.Cancel);
                throw new FluentException("Draki only supports clicking the OK or Cancel buttons of an alert/prompt.");
            }

            this.commandProvider.AlertClick(alertAccessor);
            return this;
        }

        public IActionSyntaxProvider Scroll(string selector)
        {
            return this.Scroll(this.Find(selector));
        }

        public IActionSyntaxProvider Scroll(ElementProxy element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public IActionSyntaxProvider DoubleClick(string selector)
        {
            return this.DoubleClick(this.Find(selector));
        }

        public IActionSyntaxProvider DoubleClick(ElementProxy element)
        {
            this.commandProvider.DoubleClick(element);
            return this;
        }

        public IActionSyntaxProvider RightClick(string selector)
        {
            return this.RightClick(this.Find(selector));
        }

        public IActionSyntaxProvider RightClick(ElementProxy element)
        {
            this.commandProvider.RightClick(element);
            return this;
        }

        public IActionSyntaxProvider Hover(string selector)
        {
            return this.Hover(this.Find(selector));
        }

        public IActionSyntaxProvider Hover(ElementProxy element)
        {
            this.commandProvider.Hover(element);
            return this;
        }

        public IActionSyntaxProvider Focus(string selector)
        {
            return this.Focus(this.Find(selector));
        }

        public IActionSyntaxProvider Focus(ElementProxy element)
        {
            this.commandProvider.Focus(element);
            return this;
        }

        public IActionSyntaxProvider TakeScreenshot(string screenshotName)
        {
            this.commandProvider.TakeScreenshot(screenshotName);
            return this;
        }

        public IActionSyntaxProvider Type(string text)
        {
            this.commandProvider.Type(text);
            return this;
        }

        public IActionSyntaxProvider Wait()
        {
            this.commandProvider.Wait();
            return this;
        }

        public IActionSyntaxProvider Wait(int seconds)
        {
            return this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public IActionSyntaxProvider Wait(TimeSpan timeSpan)
        {
            this.commandProvider.Wait(timeSpan);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.commandProvider.WaitUntil(conditionFunc);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction)
        {
            this.commandProvider.WaitUntil(conditionAction);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, int secondsToWait)
        {
            return this.WaitUntil(conditionFunc, TimeSpan.FromSeconds(secondsToWait));
        }

        public IActionSyntaxProvider WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionFunc, timeout);
            return this;
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, int secondsToWait)
        {
            return this.WaitUntil(conditionAction, TimeSpan.FromSeconds(secondsToWait));
        }

        public IActionSyntaxProvider WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.commandProvider.WaitUntil(conditionAction, timeout);
            return this;
        }

        private SwitchSyntaxProvider switchProvider = null;
        public SwitchSyntaxProvider Switch
        {
            get
            {
                if (switchProvider == null)
                {
                    switchProvider = new SwitchSyntaxProvider(this);
                }

                return switchProvider;
            }
        }

        public class SwitchSyntaxProvider
        {
            private readonly ActionSyntaxProvider syntaxProvider = null;

            internal SwitchSyntaxProvider(ActionSyntaxProvider syntaxProvider)
            {
                this.syntaxProvider = syntaxProvider;
            }

            /// <summary>
            /// Switch to a window by name containing or URL (can be relative such as /about -- matches on the end of the URL)
            /// </summary>
            /// <param name="windowName"></param>
            public IActionSyntaxProvider Window(string windowName)
            {
                this.syntaxProvider.commandProvider.SwitchToWindow(windowName);
                return this.syntaxProvider;
            }

            /// <summary>
            /// Switch back to the primary window
            /// </summary>
            public IActionSyntaxProvider Window()
            {
                return this.Window(string.Empty);
            }

            /// <summary>
            /// Switch to a frame/iframe via page selector or ID
            /// </summary>
            /// <param name="frameSelector"></param>
            public IActionSyntaxProvider Frame(string frameSelector)
            {
                this.syntaxProvider.commandProvider.SwitchToFrame(frameSelector);
                return this.syntaxProvider;
            }

            /// <summary>
            /// Switch back to the top-level document
            /// </summary>
            /// <returns></returns>
            public IActionSyntaxProvider Frame()
            {
                return this.Frame(string.Empty);
            }

            /// <summary>
            /// Switch focus to a previously selected frame/iframe
            /// </summary>
            /// <param name="frameElement"></param>
            /// <returns></returns>
            public IActionSyntaxProvider Frame(ElementProxy frameElement)
            {
                this.syntaxProvider.commandProvider.SwitchToFrame(frameElement);
                return this.syntaxProvider;
            }
        }
    
        #endregion

        #region Drag/Drop
        public DragDropSyntaxProvider Drag(string selector)
        {
            return this.Drag(this.Find(selector));
        }

        public DragDropSyntaxProvider Drag(ElementProxy element)
        {
            return new DragDropSyntaxProvider(this, element);
        }
        
        public class DragDropSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly ElementProxy sourceElement = null;

            internal DragDropSyntaxProvider(ActionSyntaxProvider syntaxProvider, ElementProxy element)
            {
                this.syntaxProvider = syntaxProvider;
                this.sourceElement = element;
            }

            /// <summary>
            /// End Drag/Drop operation at element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider To(string selector)
            {
                return this.To(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// End Drag/Drop operation at specified <paramref name="targetElement"/>.
            /// </summary>
            /// <param name="targetElement">IElement factory function.</param>
            public IActionSyntaxProvider To(ElementProxy targetElement)
            {
                syntaxProvider.commandProvider.DragAndDrop(sourceElement, targetElement);
                return syntaxProvider;
            }
        }

        #endregion

        #region <input />, <textarea />
        public TextAppendSyntaxProvider Append(string text)
        {
            return new TextAppendSyntaxProvider(this, text);
        }

        public TextAppendSyntaxProvider Append(dynamic nonString)
        {
            return this.Append(nonString.ToString());
        }

        public IActionSyntaxProvider Press(params Key[] keys)
        {
            commandProvider.Press(keys);
            return this;
        }


        public TextEntrySyntaxProvider Enter(string text)
        {
            return new TextEntrySyntaxProvider(this, text);
        }

        public TextEntrySyntaxProvider Enter(dynamic nonString)
        {
            return this.Enter(nonString.ToString());
        }
        #endregion

        #region <select />
        public SelectSyntaxProvider Select(string value)
        {
            return new SelectSyntaxProvider(this, value, SelectionOption.Text);
        }

        public SelectSyntaxProvider Select(Option mode, string value)
        {
            return new SelectSyntaxProvider(this, value, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);
        }

        public SelectSyntaxProvider Select(Option mode, string value, TimeSpan timeout)
        {
            return new SelectSyntaxProvider(this, value, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);
        }

        public SelectSyntaxProvider Select(params string[] values)
        {
            return new SelectSyntaxProvider(this, values, SelectionOption.Text);
        }

        public SelectSyntaxProvider Select(Option mode, params string[] values)
        {
            return new SelectSyntaxProvider(this, values, mode == Option.Text ? SelectionOption.Text : SelectionOption.Value);
        }

        public SelectSyntaxProvider Select(int index)
        {
            return new SelectSyntaxProvider(this, index, SelectionOption.Index);
        }

        public SelectSyntaxProvider Select(params int[] indices)
        {
            return new SelectSyntaxProvider(this, indices, SelectionOption.Index);
        }
        
        public class SelectSyntaxProvider
        {
            protected readonly ActionSyntaxProvider syntaxProvider = null;
            protected readonly dynamic value = null;
            internal readonly SelectionOption mode;

            internal SelectSyntaxProvider(ActionSyntaxProvider syntaxProvider, dynamic value, SelectionOption mode)
            {
                this.syntaxProvider = syntaxProvider;
                this.value = value;
                this.mode = mode;
            }

            /// <summary>
            /// Select from element matching <paramref name="selector"/>.
            /// </summary>
            /// <param name="selector">Sizzle selector.</param>
            public IActionSyntaxProvider From(string selector)
            {
                return this.From(this.syntaxProvider.Find(selector));
            }

            /// <summary>
            /// Select from specified <paramref name="element"/>.
            /// </summary>
            /// <param name="element">IElement factory function.</param>
            public IActionSyntaxProvider From(ElementProxy element)
            {
                if (this.mode == SelectionOption.Value)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectValue(element, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectValue(element, this.value);
                    }
                }
                else if (this.mode == SelectionOption.Text)
                {
                    if (this.value is string)
                    {
                        this.syntaxProvider.commandProvider.SelectText(element, this.value);
                    }
                    else if (this.value is string[])
                    {
                        this.syntaxProvider.commandProvider.MultiSelectText(element, this.value);
                    }
                }
                else if (this.value is int)
                {
                    this.syntaxProvider.commandProvider.SelectIndex(element, this.value);
                }
                else if (this.value is int[])
                {
                    this.syntaxProvider.commandProvider.MultiSelectIndex(element, this.value);
                }

                return this.syntaxProvider;
            }
        }
        #endregion

        #region Assert / Expect
        private AssertSyntaxProvider expect = null;
        public AssertSyntaxProvider Expect
        {
            get
            {
                if (this.expect == null)
                {
                    this.expect = new AssertSyntaxProvider(this.commandProvider, this.settings.ExpectIsAssert ? this.assertProvider.EnableExceptions() : this.assertProvider);
                }

                return this.expect;
            }
        }

        private AssertSyntaxProvider assert = null;
        public AssertSyntaxProvider Assert
        {
            get
            {
                if (this.assert == null)
                {
                    this.assert = new AssertSyntaxProvider(this.commandProvider, this.assertProvider.EnableExceptions());
                }

                return this.assert;
            }
        }
        #endregion

        private bool isDisposed = false;
        public bool IsDisposed()
        {
            return isDisposed;
        }

        public void Dispose()
        {
            this.isDisposed = true;
            this.commandProvider.Dispose();
        }

    }
}