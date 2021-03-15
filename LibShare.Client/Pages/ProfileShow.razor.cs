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
    public partial class ProfileShow
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        IHttpService HttpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public UserApiModel Model { get; set; } = new UserApiModel();

        Spinner LoadSpinner { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.ShowRecaptchaIcon();
            }
        }

        private async void OnSubmitHandle()
        {
            try
            {
                Model.UserName = Model.Email;
                var response = await HttpService.Post<UserApiModel, UserApiModel>(ApiUrls.ClientEditProfile, Model);
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
