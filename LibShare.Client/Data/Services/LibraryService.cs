using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IHttpService _httpService;
        public LibraryService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<PagedListApiModel<BookApiModel>> GetAllBooks(string url)
        {
            return await _httpService.Get<PagedListApiModel<BookApiModel>>(url);
        }
    }
}
