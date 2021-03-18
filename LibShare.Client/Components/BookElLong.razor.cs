using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace LibShare.Client.Components
{
    public partial class BookElLong
    {
        private static readonly Random _randomElLong = new Random();
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Parameter]
        public BookApiModel BookItem { get; set; }

        DialogModal dialogModal { get; set; }

        private string download = "Завантажити книгу";
        private string callToOwner = "Зв'язатися з власником книги";
        private string view = "Переглянути";

        private string SetRandomImage()
        {
            return $"/images/book-{_randomElLong.Next(1, 6)}.jpg";
        }

        private string SetFieldShorter(string field)
        {
            if (string.IsNullOrWhiteSpace(field))
                return null;
            if (field.Length > 50)
                return field.Substring(0, 45) + " ... ";
            else
                return field;
        }

        private string SetImageUrl()
        {
            return BookItem.Image ?? SetRandomImage();
        }

        private string ReferToBook()
        {
            return QueryHelpers.AddQueryString("/book", "bookId", BookItem.Id);
        }

        private void ReferToBookClick()
        {
            navigationManager.NavigateTo(ReferToBook());
        }

        private string ReferToDownloadBook()
        {
            return QueryHelpers.AddQueryString(ApiUrls.FileDownload, "bookId", BookItem.Id);
        }
    }
}
