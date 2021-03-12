using LibShare.Client.Data.ApiModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Pages
{
    public partial class BookAdd
    {
        public string ErrorMessage { get; set; }
        public BookApiModel BookModel { get; set; } = new BookApiModel();
        
        [Parameter]
        public string ImageUrl { get; set; }
        public string ImageBase64 { get; set; }
        public bool IsEBook { get; set; } = true;
        public bool IsPhotoLoaded { get; set; } = false;

        public ElementReference InputElementImage { get; private set; }

        async Task ImageFileSelected()
        {
            foreach (var file in await FileReaderService.CreateReference(InputElementImage).EnumerateFilesAsync())
            {
                using (MemoryStream memoryStream = await file.CreateMemoryStreamAsync(4 * 1024))
                {
                    var imageBytes = new byte[memoryStream.Length];
                    memoryStream.Read(imageBytes, 0, (int)memoryStream.Length);
                    ImageBase64 = Convert.ToBase64String(imageBytes);
                    ImageUrl = $"data:image/jpeg;base64, {ImageBase64}";
                    IsPhotoLoaded = true;
                    StateHasChanged();
                }
            }

        }

        void OnSubmitHandle()
        {

        }
    }
}
