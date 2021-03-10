using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibShare.Client.Components
{
    public partial class Pagination
    {
        [Parameter]
        public int CurrentPage { get; set; } = 1;

        [Parameter]
        public int TotalAmountPages { get; set; }

        [Parameter]
        public int Radius { get; set; } = 2;

        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        List<LinkModel> links;

        private async Task SelectedPageInternal(LinkModel link)
        {
            if (link.Page == CurrentPage)
            {
                return;
            }
            if (!link.Enabled)
            {
                return;
            }

            CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }

        protected override void OnParametersSet()
        {
            LoadPages();
            base.OnParametersSet();
        }

        private void LoadPages()
        {
            links = new List<LinkModel>();
            var isPreviousPageLinkEnabled = CurrentPage != 1;
            var previousPage = CurrentPage - 1;
            links.Add(new LinkModel(previousPage, isPreviousPageLinkEnabled, "Попередня"));
            for (int i = 1; i <= TotalAmountPages; i++)
            {
                if (i >= CurrentPage - Radius && i <= CurrentPage + Radius)
                {
                    links.Add(new LinkModel(i) { Active = CurrentPage == i });
                }
            }

            var isNextPageLinkEnabled = CurrentPage != TotalAmountPages;
            var nextPage = CurrentPage + 1;
            links.Add(new LinkModel(nextPage, isNextPageLinkEnabled, "Наступна"));
        }

        class LinkModel
        {
            public string Text { get; set; }
            public int Page { get; set; }
            public bool Enabled { get; set; } = true;
            public bool Active { get; set; } = false;

            public LinkModel(int page) : this(page, true) { }
            public LinkModel(int page, bool enabled) : this(page, enabled, page.ToString()) { }

            public LinkModel(int page, bool enabled, string text)
            {
                Page = page;
                Enabled = enabled;
                Text = text;
            }
        }
    }
}
