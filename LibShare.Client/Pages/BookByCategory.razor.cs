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
    public partial class BookByCategory
    {
        [Inject]
        ILibraryService LibraryService { get; set; }
        [Inject]
        NavigationManager navigationManager { get; set; }
        [Parameter] public int PageSize { get; set; } = 12;
        [Parameter] public int PageNumber { get; set; } = 1;
        [Parameter] public bool OnlyEbooks { get; set; } = false;
        [Parameter] public bool OnlyRealBooks { get; set; } = false;
        [Parameter] public string SortOrder { get; set; } = "1";
        public List<CategoryApiModel> Categories { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }

        public int TotalAmountPages { get; set; } = 1;
        public List<BookApiModel> BooksList { get; set; }
        public CategoryApiModel Category { get; set; } = new CategoryApiModel();
        public string CategoryId { get; set; } = "8";
        public string ErrorMessage { get; set; }

        private void GetParametersFromUrl()
        {
            var uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("chosenCategory", out var chosenCategory))
            {
                CategoryId = chosenCategory;
            }
            else
            {
                CategoryId = "8";
            }
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
            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("sortOrder", out var sortOrder))
            {
                SortOrder = sortOrder;
            }
            else
            {
                SortOrder = "1";
            }
        }

        private string SetBaseUrlQuery(string baseUrl)
        {
            var query = QueryHelpers.AddQueryString(baseUrl, "chosenCategory", CategoryId);
            query = QueryHelpers.AddQueryString(query, "pageSize", PageSize.ToString());
            query = QueryHelpers.AddQueryString(query, "pageNumber", PageNumber.ToString());
            query = QueryHelpers.AddQueryString(query, "onlyEbooks", OnlyEbooks.ToString());
            query = QueryHelpers.AddQueryString(query, "onlyRealBooks", OnlyRealBooks.ToString());
            query = QueryHelpers.AddQueryString(query, "sortOrder", SortOrder);

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
                Category = await LibraryService.GetCategory(CategoryId);
                Categories = await LibraryService.GetCategories();
                PageNumber = pageYouNeed;
                var link = SetBaseUrlQuery(ApiUrls.LibraryBooksFilter);
                Console.WriteLine("Link filter simple " + link);

                var response = await LibraryService.GetBooks(link);
                BooksList = response.List;

                PageNumber = response.CurrentPage;
                TotalAmountPages = response.TotalPages;
                
                if (response.TotalCount == 0 || BooksList == null)
                {
                    BooksList = new List<BookApiModel>();
                }

                Console.WriteLine("Get Books from Server");
            }
            catch (Exception ex)
            {
                BooksList = null;
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
                navigationManager.NavigateTo("/index");
            }
        }

        private async Task SelectedPage(int page)
        {
            await LoadBooks(page);
            PageNumber = page;
            var link = SetBaseUrlQuery("/category");
            navigationManager.NavigateTo(link);
            StateHasChanged();
        }

        private async void CategoryOnClick(string categoryId)
        {
            CategoryId = categoryId;
            await SelectedPage(1);
        }

        private async void CheckboxEBookClicked(bool checkedValue)
        {
            OnlyEbooks = checkedValue;
            await SelectedPage(1);
        }

        private async void CheckboxRealBookClicked(bool checkedValue)
        {
            OnlyRealBooks = checkedValue;
            await SelectedPage(1);
        }

        private async void SelectSortClicked(string value)
        {
            SortOrder = value;
            await SelectedPage(1);
        }
    }
}
