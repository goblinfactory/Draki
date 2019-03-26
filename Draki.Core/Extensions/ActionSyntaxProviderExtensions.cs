using Draki.Interfaces;

namespace Draki
{
    public static class ActionSyntaxProviderExtensions
    {
        public static void ExpectMultiple(this IActionSyntaxProvider I, int expected, string cssSelector)
        {
            var items = I.FindMultiple(cssSelector).Elements;
            var cnt = items.Count;
            if (cnt != expected) throw new Exceptions.FluentExpectFailedException($"Expected {expected} items, but actually found {cnt}. Selector:[{cssSelector}].");
        }
    }
}
