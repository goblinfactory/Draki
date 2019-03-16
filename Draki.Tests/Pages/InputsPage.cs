using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draki.Tests.Pages
{
    public class InputsPage : PageObject<InputsPage>
    {
        public InputsPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Inputs";
        }

        const string COLOR_ALICE_BLUE = "rgb(240, 248, 255)";
        const string COLOR_RED = "rgb(255, 248, 255)";

        public string TextControlSelector = "#text-control";

        public string TextareaControlSelector = "#textarea-control";

        public string SelectControlSelector = "#select-control";

        public string MultiSelectControlSelector = "#multi-select-control";

        public string ButtonControlSelector = "#button-control";

        public string LinkButtonControlSelector = "#linkbutton-control";
        public string LinkButtonJavascriptControlSelector = "#linkjavascript-control";

        public string InputButtonControlSelector = "#input-button-control";

        public string TextChangedTextSelector = "#text-control-changed";

        public string ButtonClickedTextSelector = "#button-clicked-text";

        public string FormGroupDivSelector = "div[class='form-group other-class']";

        public string HiddenDivSelector = "#hidden-div";

        public string HoverColor = COLOR_RED;
        public string FocusColor = COLOR_ALICE_BLUE;

        public string TextEmailControlSelector = "#text-email-control";
        public string TextSearchControlSelector = "#text-search-control";
        public string TextUrlControlSelector = "#text-url-control";
        public string TextTelControlSelector = "#text-tel-control";
        public string TextNumberControlSelector = "#text-number-control";
        public string TextPasswordControlSelector = "#text-password-control";
    }
}
