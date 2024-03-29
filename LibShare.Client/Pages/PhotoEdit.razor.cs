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
    public partial class PhotoEdit
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        Spinner SpinnerLoad { get; set; }

        [JSInvokable]
        public void InvokeMethod(string data)
        {
            Photo = data;
            StateHasChanged();
        }
        [CascadingParameter] public Toast Toast { get; set; }

        public string ErrorMessage { get; set; }
        [Inject]
        IHttpService HttpService { get; set; }

        [Parameter]
        public string Photo { get; set; } = "";

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var response = await HttpService.Get<ImageApiModel>(ApiUrls.ClientPhoto);
                if (response != null)
                {
                    Photo = response.Photo;
                }
                await JSRuntime.LoadCropper(this);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
                Console.WriteLine("Error Message: " + ErrorMessage);
            }
        }

        private async Task UpdatePhoto()
        {
            SpinnerLoad.Show();
            await HttpService.Post<ImageApiModel, ImageApiModel>(ApiUrls.ChangePhoto, new ImageApiModel() { Photo = Photo });
            Toast.ShowSuccess("Фото успішно змінено!");
            SpinnerLoad.Hide();
            NavigationManager.NavigateTo("/profile");
        }

    }
}
