using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class Contact
    {
        [Inject]
        IAuthService AuthService { get; set; }
        [Inject]
        IHttpService HttpService { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

        [Inject]
        ILibraryService LibraryService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public CallAdminApiModel Model { get; set; } = new CallAdminApiModel();

        public string Subject { get; set; } = "";
        public string Text { get; set; } = "";

        Spinner LoadSpinner { get; set; }

        async void OnSubmitHandle()
        {
            try
            {
                var message = new CallAdminApiModel();
                message.Subject = Subject;
                message.Text = Text;
                await HttpService.Post<CallAdminApiModel, MessageApiModel>(ApiUrls.CallToAdmin, message);
                Toast.ShowSuccess("Повідомлення успішно відправлено!");
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError("Помилка. Повідомлення не відправлено!");
                Console.WriteLine("Error Message: " + ErrorMessage);
            }
        }

        public string ErrorMessage { get; set; }
    }
}
