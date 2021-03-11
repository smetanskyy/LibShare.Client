using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Components
{
    public partial class ToolBox
    {
        [Parameter] public bool OnlyEbooks { get; set; }
        [Parameter] public bool OnlyRealBooks { get; set; }

        [Parameter]
        public EventCallback<bool> CheckboxEBookClicked { get; set; }

        public async Task CheckboxEBookClickedFunc(object value)
        {
            await CheckboxEBookClicked.InvokeAsync((bool)value);
        }

        [Parameter]
        public EventCallback<bool> CheckboxRealBookClicked { get; set; }
        public async Task CheckboxRealBookClickedFunc(object value)
        {
            await CheckboxRealBookClicked.InvokeAsync((bool)value);
        }
    }
}
