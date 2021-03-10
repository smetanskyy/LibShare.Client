using LibShare.Client.Data.ApiModels;
using Microsoft.AspNetCore.Components;
using System;

namespace LibShare.Client.Components
{
    public partial class BookElement
    {
        private static readonly Random _random = new Random();

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
            if (field.Length > 35)
                return field.Substring(0, 30) + " ... ";
            else
                return field;
        }

        private string SetImageUrl()
        {
            return Book.Image ?? SetRandomImage();
        }
    }
}
