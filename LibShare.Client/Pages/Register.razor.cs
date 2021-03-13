using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces.IRepositories;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class Register
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IAccountRepository accountRepository { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public RegisterApiModel Model { get; set; } = new RegisterApiModel();

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
                Model.UserName = Model.Email;
                var response = await accountRepository.RegisterUserAsync(Model);
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
