using LibShare.Client.Data.ApiModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class Register
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public RegisterApiModel Model { get; set; }

        protected override void OnInitialized()
        {
            Model = new RegisterApiModel();
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("showCaptcha");
            }
        }

        private async Task<string> GetRecaptcha(string actionName)
        {
            return await JSRuntime.InvokeAsync<string>("runCaptcha", actionName);
        }

        private async void OnSubmit()
        {
            Model.RecaptchaToken = await GetRecaptcha("OnSubmit");
            Console.WriteLine(Model.RecaptchaToken);
        }

        public void Dispose()
        {
            JSRuntime.InvokeVoidAsync("hideCaptcha");
        }
    }
}
