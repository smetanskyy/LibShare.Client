﻿using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
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
        IAuthService authService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public string ErrorMessage { get; set; }

        public LoginApiModel Model { get; set; } = new LoginApiModel();

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
                await authService.Login(Model);
                LoadSpinner.Hide();

                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("returnUrl", out var returnUrl))
                {
                    Console.WriteLine("Return Url: " + returnUrl);
                    NavigationManager.NavigateTo(returnUrl);
                }
                else
                {
                    NavigationManager.NavigateTo("/index");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LoadSpinner.Hide();
                StateHasChanged();
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