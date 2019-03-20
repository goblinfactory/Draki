using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draki.Tests.Pages
{
    public class DragPage : PageObject<DragPage>
    {
        public DragPage(FluentTest test)
            : base(test)
        {
            this.Url = "/DragAndDrop";
        }
    }
}
