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
    public partial class RestorePasswordChangePasswor
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

        [Inject]
        IHttpService _httpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public RestoreApiModel Model { get; set; } = new RestoreApiModel();

        public string ErrorMessage { get; set; }

        [Inject]
        IAuthService authService { get; set; }

        private Spinner LoadSpinner { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.ShowRecaptchaIcon();
            }
        }

        protected override void OnInitialized()
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
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
            }
        }

        private async void OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                Model.RecaptchaToken = await JSRuntime.GetRecaptcha("OnSubmit");
                Console.WriteLine(Model.RecaptchaToken);
                var response = await _httpService.Post<RestoreApiModel, TokenApiModel>(ApiUrls.RestorePassworPartTwoUrl, Model);
                await authService.UpdateToken(response);
                LoadSpinner.Hide();
                Toast.ShowSuccess("Пароль успішно змінено!");
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LoadSpinner.Hide();
                Toast.ShowError(ex.Message);
                StateHasChanged();
            }
        }

        public void ClearErrorMessage()
        {
            ErrorMessage = null;
        }

        public void Dispose()
        {
            JSRuntime.HideRecaptchaIcon();
        }
    }
}
