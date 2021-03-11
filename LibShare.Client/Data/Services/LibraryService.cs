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

        public async Task<BookApiModel> CreateBookAsync(string url, BookApiModel book)
        {
            return await _httpService.Post<BookApiModel, BookApiModel>(url, book);
        }

        public async Task<MessageApiModel> DeleteBookAsync(string url)
        {
            return await _httpService.Put<string, MessageApiModel>(url, "Delete Book");
        }

        public async Task<PagedListApiModel<BookApiModel>> GetBooks(string url)
        {
            return await _httpService.Get<PagedListApiModel<BookApiModel>>(url);
        }

        public async Task<BookApiModel> GetBookByIdAsync(string url)
        {
            return await _httpService.Get<BookApiModel>(url);
        }

        public async Task<List<CategoryApiModel>> GetCategories(string url)
        {
            return await _httpService.Get<List<CategoryApiModel>>(url);
        }

        public async Task<CategoryApiModel> GetCategory(string url)
        {
            return await _httpService.Get<CategoryApiModel>(url);
        }

        public async Task<BookApiModel> UpdateBookAsync(string url, BookApiModel book)
        {
            return await _httpService.Post<BookApiModel, BookApiModel>(url, book);
        }
    }
}
