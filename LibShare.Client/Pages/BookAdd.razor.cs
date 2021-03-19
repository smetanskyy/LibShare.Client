using LibShare.Client.Components;
using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class BookAdd
    {
        [Inject] ILibraryService LibraryService { get; set; }
        [Inject] IAuthService authService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        Spinner LoadSpinner { get; set; }
        [CascadingParameter] public Toast Toast { get; set; }
        public string ErrorMessage { get; set; }
        public BookApiModel BookModel { get; set; } = new BookApiModel();
        public int Year { get; set; } = 2000;
        public string ImageBase64 { get; set; }
        public string ImageUrl { get; set; }
        public bool IsEBook { get; set; } = true;
        public bool IsPhotoLoaded { get; set; } = false;

        public List<CategoryApiModel> Categories { get; set; }

        public ElementReference InputElementImage { get; private set; }
        public ElementReference InputElementFile { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            Categories = await LibraryService.GetCategories();
        }

        bool IsFileSelected = false;
        void FileSelected()
        {
            IsFileSelected = true;
            StateHasChanged();
        }
        async Task FileUpload(string bookId)
        {
            foreach (var file in await FileReaderService.CreateReference(InputElementFile).EnumerateFilesAsync())
            {
                if (file != null)
                {
                    var fileInfo = await file.ReadFileInfoAsync();
                    using (var ms = await file.CreateMemoryStreamAsync(4 * 1024))
                    {
                        var content = new MultipartFormDataContent();
                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        content.Add(new StreamContent(ms, Convert.ToInt32(ms.Length)), "file", fileInfo.Name);
                        Console.WriteLine("File пішов");
                        await LibraryService.UploadFile(bookId, content);
                    }
                }
            }
        }
        bool IsImageSelected = false;
        async Task ImageFileSelected()
        {
            LoadSpinner.Show();
            IsImageSelected = true;
            foreach (var file in await FileReaderService.CreateReference(InputElementImage).EnumerateFilesAsync())
            {
                using (MemoryStream memoryStream = await file.CreateMemoryStreamAsync(4 * 1024))
                {
                    var imageBytes = new byte[memoryStream.Length];
                    memoryStream.Read(imageBytes, 0, (int)memoryStream.Length);
                    ImageBase64 = Convert.ToBase64String(imageBytes);
                    ImageUrl = $"data:image/jpeg;base64, {ImageBase64}";
                    IsPhotoLoaded = true;
                }
            }

            StateHasChanged();
            LoadSpinner.Hide();
        }

        async Task OnSubmitHandle()
        {
            LoadSpinner.Show();
            try
            {
                if (BookModel.CategoryId == null || BookModel.CategoryId.Contains("..."))
                {
                    throw new Exception("Виберіть категорію!");
                }
                BookModel.Year = Year.ToString();
                BookModel.Image = null;
                BookModel.DateCreate = DateTime.Now;
                BookModel.IsEbook = IsEBook;

                await authService.RefreshToken();
                var result = await LibraryService.CreateBookAsync(BookModel);

                await LibraryService.UploadBookImage(result.Id, new ImageApiModel { Photo = ImageBase64 });
                await FileUpload(result.Id);
                Toast.ShowSuccess("Книга успішно додана!");
                NavigationManager.NavigateTo(QueryHelpers.AddQueryString("/book", "bookId", result.Id));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                Toast.ShowError(ex.Message);
                Console.WriteLine("Error Message: " + ErrorMessage);
            }
            finally
            {
                LoadSpinner.Hide();
            }
        }
    }
}
