using System.Collections.Generic;

namespace LibShare.Client.Data.ApiModels
{
    public class PagedListApiModel<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public List<T> List { get; set; }
    }
}
