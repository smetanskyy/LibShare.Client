using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Constants
{
    public static class ApiUrls
    {
        public static string ApiBaseUrl { get; set; } = "http://localhost:5050";
        //public static string ApiBaseUrl { get; set; } = "http://20.67.117.174:5000";
        public static string LoginUrl { get; set; } = ApiBaseUrl + "/api/account/login";
        public static string RegisterUrl { get; set; } = ApiBaseUrl + "/api/account/register";
        public static string RefreshTokenUrl { get; set; } = ApiBaseUrl + "/api/account/refreshtoken";
        public static string RestorePassworPartOneUrl { get; set; } = ApiBaseUrl + "/api/account/restore-link";
        public static string RestorePassworPartTwoUrl { get; set; } = ApiBaseUrl + "/api/account/restore-password";
        public static string ChangePasswordUrl { get; set; } = ApiBaseUrl + "/api/account/change-password";
        public static string ConfirmEmailPartOneUrl { get; set; } = ApiBaseUrl + "/api/account/confirm-link";
        public static string ConfirmEmailPartTwoUrl { get; set; } = ApiBaseUrl + "/api/account/confirm-mail";
        public static string DeleteMeUrl { get; set; } = ApiBaseUrl + "/api/account/delete-me";
        public static string Logout { get; set; } = ApiBaseUrl + "/api/account/logout";
        public static string ClientInfo { get; set; } = ApiBaseUrl + "/api/client/info";
        public static string ClientPhoto { get; set; } = ApiBaseUrl + "/api/client/photo";
        public static string ChangePhoto { get; set; } = ApiBaseUrl + "/api/client/change-photo";
        public static string ClientEditProfile { get; set; } = ApiBaseUrl + "/api/client/set-profile";
        public static string FileUpload { get; set; } = ApiBaseUrl + "/api/file/upload";
        public static string FileDownload { get; set; } = ApiBaseUrl + "/api/file/download";
        public static string LibraryAllCategories { get; set; } = ApiBaseUrl + "/api/library/all-categories";
        public static string LibraryCategory { get; set; } = ApiBaseUrl + "/api/library/category";
        public static string LibraryAllBooks { get; set; } = ApiBaseUrl + "/api/library/books";
        public static string LibraryAddBook { get; set; } = ApiBaseUrl + "/api/library/add-book";
        public static string LibraryBooksMultifilter { get; set; } = ApiBaseUrl + "/api/library/books-multifilter";
        public static string LibraryBooksFilter { get; set; } = ApiBaseUrl + "/api/library/books-filter";
        public static string LibraryBooksSearch { get; set; } = ApiBaseUrl + "/api/library/books-search";
        public static string LibraryBook { get; set; } = ApiBaseUrl + "/api/library/book";
        public static string LibraryBookUpdate { get; set; } = ApiBaseUrl + "/api/library/book-update";
        public static string LibraryBookDelete { get; set; } = ApiBaseUrl + "/api/library/book-delete";
        public static string LibraryBooksUser { get; set; } = ApiBaseUrl + "/api/library/books-user";
    }
}
