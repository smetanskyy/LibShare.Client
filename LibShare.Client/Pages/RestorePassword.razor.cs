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
    public partial class RestorePassword
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IHttpService _httpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public EmailApiModel Model { get; set; } = new EmailApiModel();

        public string ErrorMessage { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

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
                var response = await _httpService.Post<EmailApiModel, MessageApiModel>(ApiUrls.RestorePassworPartOneUrl, Model);
                LoadSpinner.Hide();
                Toast.ShowSuccess("Провірте пошту і прейдіть по посиланню!");
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LoadSpinner.Hide();
                StateHasChanged();
                Toast.ShowError(ex.Message);
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
