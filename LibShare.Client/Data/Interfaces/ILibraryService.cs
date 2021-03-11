using LibShare.Client.Data.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    interface ILibraryService
    {
        Task<CategoryApiModel> GetCategory(string url);
        Task<List<CategoryApiModel>> GetCategories(string url);
        Task<BookApiModel> CreateBookAsync(string url, BookApiModel book);
        Task<BookApiModel> GetBookByIdAsync(string url);
        Task<BookApiModel> UpdateBookAsync(string url, BookApiModel book);
        Task<MessageApiModel> DeleteBookAsync(string url);
        Task<PagedListApiModel<BookApiModel>> GetBooks(string url);
    }
}
