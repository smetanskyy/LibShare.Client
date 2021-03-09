using LibShare.Client.Data.ApiModels;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class Login
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public LoginApiModel Model { get; set; }
        protected override void OnInitialized()
        {
            Model = new LoginApiModel();
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.ShowRecaptchaIcon();
            }
        }

        private async void OnSubmit()
        {
            Model.RecaptchaToken = await JSRuntime.GetRecaptcha("OnSubmit");
            Console.WriteLine(Model.RecaptchaToken);
            Console.WriteLine("Form Submitted Successfully!");
        }

        public void Dispose()
        {
            JSRuntime.HideRecaptchaIcon();
        }
    }
}