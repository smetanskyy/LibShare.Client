using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using System;

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
            LoadSpinner.Show();
            try
            {
                Model.Email = Subject;
                Model.Text = Text;
                await HttpService.Post<CallAdminApiModel, MessageApiModel>(ApiUrls.CallToAdmin, Model);
                Toast.ShowSuccess("Повідомлення успішно відправлено!");
                NavigationManager.NavigateTo("/index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError("Помилка. Повідомлення не відправлено!");
                Console.WriteLine("Error Message: " + ErrorMessage);
            }
            finally
            {
                LoadSpinner.Hide();
            }
        }

        public string ErrorMessage { get; set; }
    }
}
