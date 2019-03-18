﻿using System;
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

        public string HoverColor = Colors.RED;
        public string ButtonFocusColor = Colors.SILVER;

        public string TextEmailControlSelector = "#text-email-control";
        public string TextSearchControlSelector = "#text-search-control";
        public string TextUrlControlSelector = "#text-url-control";
        public string TextTelControlSelector = "#text-tel-control";
        public string TextNumberControlSelector = "#text-number-control";
        public string TextPasswordControlSelector = "#text-password-control";
    }
}
