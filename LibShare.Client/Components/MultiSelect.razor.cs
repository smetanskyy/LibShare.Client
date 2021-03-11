using LibShare.Client.Data.ApiModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LibShare.Client.Components
{
    public partial class MultiSelect
    {
        [Parameter]
        public bool DisabledSelect { get; set; } = false;

        [Parameter]
        public List<CategoryApiModel> Categories { get; set; }

    }
}
