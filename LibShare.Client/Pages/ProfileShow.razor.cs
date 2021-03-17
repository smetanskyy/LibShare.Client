using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;

namespace LibShare.Client.Pages
{
    public partial class ProfileShow
    {
        [Inject]
        IHttpService HttpService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public UserApiModel Model { get; set; } = new UserApiModel();

        Spinner LoadSpinner { get; set; }

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
    }
}
