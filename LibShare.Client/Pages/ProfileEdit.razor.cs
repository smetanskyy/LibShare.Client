using LibShare.Client.Components;
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
                ErrorMessage = ex.Message;
            }
        }

        private async void OnSubmitHandle()
        {
            try
            {
                Model.UserName = Model.Email;
                var response = await HttpService.Post<UserApiModel, UserApiModel>(ApiUrls.ClientEditProfile, Model);
                LoadSpinner.Hide();
                NavigationManager.NavigateTo("/profile");
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
    }
}
