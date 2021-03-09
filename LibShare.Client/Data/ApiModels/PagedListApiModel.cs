﻿using System.Collections.Generic;

namespace LibShare.Client.Data.ApiModels
{
    public class PagedListApiModel<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public List<T> List { get; set; }
    }
}
