using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Draki.Exceptions;

namespace Draki.Interfaces
{
    public interface ICommandProvider : IDisposable
    {
        Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }
        Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }
        Uri Url { get; }
        string Source { get; }

        void Navigate(Uri url);
        ElementProxy Find(string selector);
        ElementProxy FindMultiple(string selector);

        bool QuickExists(string cssSelector);
        string GetTitle();

        void Click(ElementProxy element);
        void DoubleClick(ElementProxy element);
        void RightClick(ElementProxy element);
        void Hover(ElementProxy element);
        void Focus(ElementProxy element);
        void DragAndDrop(ElementProxy source, ElementProxy target);
        void EnterText(ElementProxy element, string text);
        void EnterTextWithoutEvents(ElementProxy element, string text);
        void AppendText(ElementProxy element, string text);
        void AppendTextWithoutEvents(ElementProxy element, string text);

        void SelectText(ElementProxy element, string optionText);
        void SelectValue(ElementProxy element, string optionValue);
        void SelectIndex(ElementProxy element, int optionIndex);

        void MultiSelectText(ElementProxy element, string[] optionTextCollection);
        void MultiSelectValue(ElementProxy element, string[] optionValues);
        void MultiSelectIndex(ElementProxy element, int[] optionIndices);

        void TakeScreenshot(string screenshotName);

        void Wait();
        void Wait(TimeSpan timeSpan);
        void WaitUntil(Expression<Func<bool>> conditionFunc);
        void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout);
        void WaitUntil(Expression<Action> conditionAction);
        void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout);

        void Press(params Key[] keys);
        void Type(string text);

        void SwitchToFrame(string frameName);
        void SwitchToFrame(ElementProxy frameElement);
        void SwitchToWindow(string windowName);
        void AlertClick(Alert accessor);
        void AlertText(Action<string> matchFunc);
        void AlertEnterText(string text);
        void Visible(ElementProxy element, Action<bool> action);

        void CssPropertyValue(ElementProxy element, string propertyName, Action<bool, string> action);

        void Act(CommandType commandType, Action action);

        ICommandProvider WithConfig(FluentSettings settings);
    }
}
