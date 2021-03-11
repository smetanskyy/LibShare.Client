using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class TopSeller
    {
        [Inject]
        ILibraryService LibraryService { get; set; }

        private Spinner LoadSpinner { get; set; }
        [Parameter] public int PageSize { get; set; } = 12;
        [Parameter] public int PageNumber { get; set; } = 1;
        [Parameter] public bool OnlyEbooks { get; set; } = false;
        [Parameter] public bool OnlyRealBooks { get; set; } = false;

        public int TotalAmountPages { get; set; } = 1;
        public List<BookApiModel> BooksList { get; set; }
        public string ErrorMessage { get; set; }

        private void GetParametersFromUrl()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("pageSize", out var pageSize))
            {
                PageSize = int.Parse(pageSize);
            }
            else
            {
                PageSize = 12;
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("pageNumber", out var pageNumber))
            {
                PageNumber = int.Parse(pageNumber);
            }
            else
            {
                PageNumber = 1;
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("onlyEbooks", out var onlyEbooks))
            {
                OnlyEbooks = bool.Parse(onlyEbooks);
            }
            else
            {
                OnlyEbooks = false;
            }
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("onlyRealBooks", out var onlyRealBooks))
            {
                OnlyRealBooks = bool.Parse(onlyRealBooks);
            }
            else
            {
                OnlyRealBooks = false;
            }
        }

        private string SetBaseUrlQuery(string baseUrl)
        {
            var query = QueryHelpers.AddQueryString(baseUrl, "pageSize", PageSize.ToString());
            query = QueryHelpers.AddQueryString(query, "pageNumber", PageNumber.ToString());
            query = QueryHelpers.AddQueryString(query, "onlyEbooks", OnlyEbooks.ToString());
            query = QueryHelpers.AddQueryString(query, "onlyRealBooks", OnlyRealBooks.ToString());
            return query;
        }

        protected override async Task OnInitializedAsync()
        {
            GetParametersFromUrl();
            Console.WriteLine("Hello from OnInitializedAsync Books from Server");
            await LoadBooks(PageNumber);
        }

        private async Task LoadBooks(int pageYouNeed)
        {
            try
            {
                PageNumber = pageYouNeed;
                var link = SetBaseUrlQuery(ApiUrls.LibraryAllBooks);

                var response = await LibraryService.GetBooks(link);
                BooksList = response.List;

                PageNumber = response.CurrentPage;
                TotalAmountPages = response.TotalPages;

                Console.WriteLine("Get Books from Server");
            }
            catch (Exception ex)
            {
                BooksList = null;
                ErrorMessage = ex.Message;
            }
        }

        private async Task SelectedPage(int page)
        {
            await LoadBooks(page);
            PageNumber = page;
            var link = SetBaseUrlQuery("/top-seller");
            navigationManager.NavigateTo(link);
        }
    }
}
