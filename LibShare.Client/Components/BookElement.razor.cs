using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Net;
using System.Net.Http;

namespace LibShare.Client.Components
{
    public partial class BookElement
    {
        private static readonly Random _random = new Random();
        [Inject]
        NavigationManager navigationManager { get; set; }

        [Inject]
        HttpClient httpClient { get; set; }

        [Parameter]
        public BookApiModel Book { get; set; }

        private string download = "Завантажити книгу";
        private string callToOwner = "Зв'язатися з власником книги";
        private string view = "Переглянути";

        private string SetRandomImage()
        {
            return $"/images/book-{_random.Next(1, 6)}.jpg";
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
            return Book.Image ?? SetRandomImage();
        }

        private string ReferToBook()
        {
            return QueryHelpers.AddQueryString("/book", "bookId", Book.Id);
        }

        private void ReferToBookClick()
        {
            navigationManager.NavigateTo(ReferToBook());
        }

        private string ReferToDoawloadBook()
        {
            return QueryHelpers.AddQueryString(ApiUrls.FileDownload, "bookId", Book.Id);
        }
    }
}
