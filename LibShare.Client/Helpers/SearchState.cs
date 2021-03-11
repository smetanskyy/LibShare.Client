using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Helpers
{
    public class SearchState
    {
        private readonly ILibraryService _libraryService;

        public List<BookApiModel> SelectedBooks { get; private set; }

        public event Action OnChange;

        public SearchState(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async void SetBookList(string search)
        {
            var query = QueryHelpers.AddQueryString(ApiUrls.LibraryBooksSearch, "search", search);
            await _libraryService.GetBooks(query);

            SelectedBooks = new List<BookApiModel>() {
            new BookApiModel() { Title = "Gool .... " },
            new BookApiModel() { Title = "Gool .... " },
            new BookApiModel() { Title = "Gool .... " },
            new BookApiModel() { Title = "Gool .... " }
            };
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
