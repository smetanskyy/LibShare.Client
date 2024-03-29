﻿using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class ProfileEdit
    {
        [Inject]
        IHttpService HttpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public UserApiModel Model { get; set; } = new UserApiModel();
        [CascadingParameter] public Toast Toast { get; set; }

        public string ErrorMessage { get; set; }

        Spinner LoadSpinner { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Model = await HttpService.Get<UserApiModel>(ApiUrls.ClientInfo);
            }
            catch (Exception ex)
            {
                Toast.ShowError(ex.Message);
                ErrorMessage = ex.Message;
            }
        }

        private async void OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                Model.UserName = Model.Email;
                var response = await HttpService.Post<UserApiModel, UserApiModel>(ApiUrls.ClientEditProfile, Model);
                LoadSpinner.Hide();
                Toast.ShowSuccess("Профайл успішно змінено!");
                NavigationManager.NavigateTo("/profile");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LoadSpinner.Hide();
                StateHasChanged();
                Toast.ShowError(ex.Message);
            }
            finally
            {
                LoadSpinner.Hide();
            }
        }

        public void ClearErrorMessage()
        {
            ErrorMessage = null;
        }
    }
}
