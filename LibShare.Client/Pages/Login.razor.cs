using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces.IRepositories;
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

        [Inject]
        IAccountRepository accountRepository { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        public LoginApiModel Model { get; set; } = new LoginApiModel();

        [CascadingParameter]
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
                var response = await accountRepository.LoginUserAsync(Model);
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                StateHasChanged();
                LoadSpinner.Hide();
            }
        }

        void ClearErrorMessage()
        {
            ErrorMessage = null;
        }

        public void Dispose()
        {
            JSRuntime.HideRecaptchaIcon();
        }
    }
}