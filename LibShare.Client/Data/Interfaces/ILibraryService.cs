using LibShare.Client.Data.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    interface ILibraryService
    {
        //Task<IEnumerable<CategoryApiModel>> GetCategories();
        //Task<BookApiModel> CreateBookAsync(string url, BookApiModel book);
        //Task<BookApiModel> GetBookByIdAsync(string url);
        //Task<BookApiModel> UpdateBookAsync(string url, BookApiModel book);
        //Task<MessageApiModel> DeleteBookAsync(string url);
        Task<PagedListApiModel<BookApiModel>> GetAllBooks(string url);
        //Task<PagedListApiModel<BookApiModel>> Search(string url);
        //Task<PagedListApiModel<BookApiModel>> FilterByMultiCategory(string url);
        //Task<PagedListApiModel<BookApiModel>> FilterByCategory(string url);
        //Task<PagedListApiModel<BookApiModel>> GetBooksByUserId(string url);
    }
}
