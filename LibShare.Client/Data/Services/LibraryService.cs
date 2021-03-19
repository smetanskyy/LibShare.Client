using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly IHttpService _httpService;
        private readonly HttpClient _httpClient;
        public LibraryService(IHttpService httpService, HttpClient httpClient)
        {
            _httpService = httpService;
            _httpClient = httpClient;
        }

        public async Task<BookApiModel> CreateBookAsync(BookApiModel book)
        {
            return await _httpService.Post<BookApiModel, BookApiModel>(ApiUrls.LibraryAddBook, book);
        }

        public async Task<MessageApiModel> DeleteBookAsync(string bookId)
        {
            var urlServer = QueryHelpers.AddQueryString(ApiUrls.LibraryCategory, "bookId", bookId);
            return await _httpService.Put<string, MessageApiModel>(urlServer, "Delete Book");
        }
        
        public async Task<PagedListApiModel<BookApiModel>> GetBooks(string url)
        {
            return await _httpService.Get<PagedListApiModel<BookApiModel>>(url);
        }

        public async Task<BookApiModel> GetBookByIdAsync(string bookId)
        {
            var urlServer = QueryHelpers.AddQueryString(ApiUrls.LibraryBook, "bookId", bookId);
            return await _httpService.Get<BookApiModel>(urlServer);
        }

        public async Task<List<CategoryApiModel>> GetCategories()
        {
            return await _httpService.Get<List<CategoryApiModel>>(ApiUrls.LibraryAllCategories);
        }

        public async Task<CategoryApiModel> GetCategory(string categoryId)
        {
            var urlServer = QueryHelpers.AddQueryString(ApiUrls.LibraryCategory, "categoryId", categoryId);
            return await _httpService.Get<CategoryApiModel>(urlServer);
        }

        public async Task<BookApiModel> UpdateBookAsync(BookApiModel book)
        {
            return await _httpService.Post<BookApiModel, BookApiModel>(ApiUrls.LibraryBookUpdate, book);
        }

        public async Task GetBookFileAsync(string bookId)
        {
            var query = QueryHelpers.AddQueryString(ApiUrls.FileDownload, "bookId", bookId);
            await _httpClient.GetAsync(query);
        }

        public async Task UploadFile(string bookId, MultipartFormDataContent content)
        {
            var query = QueryHelpers.AddQueryString(ApiUrls.FileUpload, "bookId", bookId);
            await _httpClient.PostAsync(query, content);
        }

        public async Task UploadBookImage(string bookId, ImageApiModel model)
        {
            var query = QueryHelpers.AddQueryString(ApiUrls.FileUploadImage, "bookId", bookId);
            await _httpService.Post<ImageApiModel, MessageApiModel>(query, model);
        }
    }
}
