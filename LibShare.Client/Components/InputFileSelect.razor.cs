using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibShare.Client.Components
{
    public partial class InputFileSelect
    {
        [Parameter]
        public string Label { get; set; } = "Загрузка книги користувача";


        public ElementReference InputElement { get; private set; }

        [Parameter]
        public EventCallback<string> OnSelectedFile { get; set; }

        async Task FileSelected()
        {
            foreach (var file in await FileReaderService.CreateReference(InputElement).EnumerateFilesAsync())
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
                        await libraryService.UploadFile("boooookkkkkk", content);
                    }
                }
            }
        }
    }
}
