using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Helpers;
using LibShare.Client.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Shared
{
    public partial class AboveNavMenu
    {
        [Inject]
        NavigationManager navigation { get; set; }

        [Inject]
        public SearchState searchState { get; set; }

        public SearchModel SearchModel { get; set; } = new SearchModel() { SearchField = "" };

        private void Search()
        {
            var query = "/search";

            if (!string.IsNullOrWhiteSpace(SearchModel.SearchField))
            {
                query = QueryHelpers.AddQueryString(query, "search", SearchModel.SearchField);
            }

            searchState.SetSearchField(SearchModel.SearchField.Trim());
            SearchModel.SearchField = "";
            navigation.NavigateTo(query);
        }
    }

    public class SearchModel
    {
        public string SearchField { get; set; }
    }
}
