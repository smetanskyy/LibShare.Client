using LibShare.Client.Helpers;
using LibShare.Client.Pages;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Shared
{
    public partial class AboveNavMenu
    {
        [Inject]
        public SearchState searchState { get; set; }

        [Inject]
        NavigationManager navigation { get; set; }
        private string searchField;
        private void Search()
        {
            booksPage.BooksList = 
            navigation.NavigateTo("/book");
        }
    }
}
