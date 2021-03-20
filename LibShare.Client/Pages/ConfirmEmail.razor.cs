using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class ConfirmEmail
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

        [Inject]
        IHttpService _httpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public ConfirmApiModel Model { get; set; } = new ConfirmApiModel();

        public string ErrorMessage { get; set; }
        public string Success { get; set; }

        [Inject]
        IAuthService authService { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.ShowRecaptchaIcon();
            }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("email", out var email))
                {
                    Model.Email = email;
                }
                else
                {
                    throw new Exception("Посилання недійсне!");
                }
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("token", out var token))
                {
                    Model.Token = token;
                }
                else
                {
                    throw new Exception("Посилання недійсне!");
                }

                Model.RecaptchaToken = await JSRuntime.GetRecaptcha("OnSubmit");
                await _httpService.Post<ConfirmApiModel, MessageApiModel>(ApiUrls.ConfirmEmailPartTwoUrl, Model);
                Toast.ShowSuccess("Електронна пошта успішно підтверджена!");
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
                NavigationManager.NavigateTo("/register");
            }
        }

        public void Dispose()
        {
            JSRuntime.HideRecaptchaIcon();
        }
    }
}
