using LibShare.Client.Data.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface ILibraryService
    {
        Task<CategoryApiModel> GetCategory(string categoryId);
        Task<List<CategoryApiModel>> GetCategories();
        Task<BookApiModel> CreateBookAsync(BookApiModel book);
        Task<BookApiModel> GetBookByIdAsync(string bookId);
        Task<BookApiModel> UpdateBookAsync(BookApiModel book);
        Task<MessageApiModel> DeleteBookAsync(string bookId);
        Task<PagedListApiModel<BookApiModel>> GetBooks(string url);
        Task GetBookFileAsync(string url);
        Task UploadFile(string bookId, MultipartFormDataContent content);
    }
}
