using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class ChangePassword
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IHttpService _httpService { get; set; }

        IAccountService accountService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public ChangePasswordApiModel Model { get; set; } = new ChangePasswordApiModel();

        public string ErrorMessage { get; set; }

        private Spinner LoadSpinner { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.ShowRecaptchaIcon();
            }
        }

        private async void OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                Model.RecaptchaToken = await JSRuntime.GetRecaptcha("OnSubmit");
                var response = _httpService.Post<ChangePasswordApiModel, TokenApiModel>(ApiUrls.ChangePasswordUrl, Model);
                LoadSpinner.Hide();
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LoadSpinner.Hide();
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
