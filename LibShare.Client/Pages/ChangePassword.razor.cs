﻿using LibShare.Client.Components;
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
        [CascadingParameter] public Toast Toast { get; set; }

        [Inject]
        IHttpService _httpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public ChangePasswordApiModel Model { get; set; } = new ChangePasswordApiModel();

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

        private async void OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                Model.RecaptchaToken = await JSRuntime.GetRecaptcha("OnSubmit");
                Console.WriteLine(Model.RecaptchaToken);
                var response = await _httpService.Post<ChangePasswordApiModel, TokenApiModel>(ApiUrls.ChangePasswordUrl, Model);
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
