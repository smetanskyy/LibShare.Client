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
        IHttpService HttpService { get; set; }

        [Inject]
        ILibraryService LibraryService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public UserApiModel Model { get; set; } = new UserApiModel();

        Spinner LoadSpinner { get; set; }

        BookApiModel BookItem { get; set; }

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
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public string ErrorMessage { get; set; }

    }
}
