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
    public partial class CallToOwner
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
        public UserApiModel Model { get; set; } = new UserApiModel();

        public string Subject { get; set; } = "Замовлення книги";
        public string Text { get; set; } = "Прошу відправити мені книгу ";

        Spinner LoadSpinner { get; set; }

        BookApiModel BookItem { get; set; } = new BookApiModel();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string bookIdFromUrl = "";
                var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("bookId", out var bookId))
                {
                    bookIdFromUrl = bookId;
                }
                else
                {
                    NavigationManager.NavigateTo("/books");
                }

                if (string.IsNullOrWhiteSpace(bookIdFromUrl))
                    NavigationManager.NavigateTo("/books");

                Console.WriteLine("bookIdFromUrl " + bookIdFromUrl);
                BookItem = await LibraryService.GetBookByIdAsync(bookIdFromUrl);
                Model = await HttpService.Get<UserApiModel>(ApiUrls.ClientInfo);
                Text = Text + BookItem.Title;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }


        async void OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                var message = new CallOwnerApiModel();
                message.Subject = Subject;
                message.Text = Text;
                message.BookId = BookItem.Id;
                await AuthService.RefreshToken();
                await HttpService.Post<CallOwnerApiModel, MessageApiModel>(ApiUrls.CallToOwner, message);
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
