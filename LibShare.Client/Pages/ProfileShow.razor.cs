using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class ProfileShow
    {
        [Inject] IHttpService HttpService { get; set; }
        [Inject] IAuthService AuthService { get; set; }
        
        [Inject] IAccountService AccountService { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter]
        public UserApiModel Model { get; set; } = new UserApiModel();

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

        public string ErrorMessage { get; set; }

        void GoToChangeProfile()
        {
            NavigationManager.NavigateTo("/profile-edit");
        }

        void GoToChangePhoto()
        {
            NavigationManager.NavigateTo("/photo-edit");
        }

        void GoToChangePassword()
        {
            NavigationManager.NavigateTo("/change-password");
        }

        DialogModalDeleteUser modal { get; set; }

        async void DeleteAccount()
        {
            try
            {
                await AccountService.DeleteMe();
                Toast.ShowSuccess("Обліковий запис успішно видалено!");
                await AuthService.Logout();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
            }
        }
    }
}
