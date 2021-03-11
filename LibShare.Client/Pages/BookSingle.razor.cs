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
    public partial class BookSingle
    {
        private static readonly Random _random = new Random();

        private string download = "Завантажити книгу";
        private string callToOwner = "Зв'язатися з власником книги";

        public BookApiModel BookItem { get; set; } = new BookApiModel();

        [Inject]
        ILibraryService LibraryService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string bookIdFromUrl = "book1";
                var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("bookId", out var bookId))
                {
                    bookIdFromUrl = bookId;
                }
                Console.WriteLine("bookIdFromUrl " + bookIdFromUrl);

                var urlServer = QueryHelpers.AddQueryString(ApiUrls.LibraryBook, "bookId", bookIdFromUrl);
                BookItem = await LibraryService.GetBookByIdAsync(urlServer);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        private string SetImageUrl()
        {
            return BookItem.Image ?? SetRandomImage();
        }
        private string SetRandomImage()
        {
            return $"/images/book-{_random.Next(1, 6)}.jpg";
        }
    }
}
