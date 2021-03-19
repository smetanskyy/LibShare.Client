using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace LibShare.Client.Components
{
    public partial class Toast
    {
        [Parameter] public string Header { get; set; }
        public string Body { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        [Inject] IJSRuntime jsRuntime { get; set; }

        public bool IsSuccess { get; set; } = true;

        public void Show()
        {
            StateHasChanged();
            jsRuntime.InvokeVoidAsync("showToast");
        }

        public void ShowError(string message)
        {
            IsSuccess = false;
            Body = message;
            Show();
        }

        public void ShowSuccess(string message)
        {
            IsSuccess = true;
            Body = message;
            Show();
        }

        public void Hide()
        {
            StateHasChanged();
            jsRuntime.InvokeVoidAsync("hideToast");
        }
    }
}
