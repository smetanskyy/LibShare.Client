﻿using LibShare.Client.Components;
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
        private string download = "Завантажити книгу";
        private string callToOwner = "Зв'язатися з власником книги";
        [CascadingParameter] public Toast Toast { get; set; }

        public BookApiModel BookItem { get; set; }
        public string ReferDownloadBook { get; set; }

        [Inject]
        ILibraryService LibraryService { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        DialogModal dialogModal { get; set; }

        public string ErrorMessage { get; set; }

        private string ReferToCallOwner()
        {
            return QueryHelpers.AddQueryString("/call-to-owner", "bookId", BookItem.Id);
        }

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
                else
                {
                    bookIdFromUrl = "book1";
                }

                Console.WriteLine("bookIdFromUrl " + bookIdFromUrl);
                BookItem = await LibraryService.GetBookByIdAsync(bookIdFromUrl);
                ReferDownloadBook = QueryHelpers.AddQueryString(ApiUrls.FileDownload, "bookId", BookItem.Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
            }
        }
    }
}
