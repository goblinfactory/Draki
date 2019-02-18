namespace Draki.Tests.Pages
{
    public class HomePage : PageObject<HomePage>
    {
        public HomePage(FluentTest test)
            : base(test)
        {
            this.Url = "/";
        }

        //public string TriggerAlertSelector = "#trigger-alert";
        //public string TriggerConfirmSelector = "#trigger-confirm";
        //public string TriggerPromptSelector = "#trigger-prompt";
        //public string ResultSelector = "#result";
    }
}
