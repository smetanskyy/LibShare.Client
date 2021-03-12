using LibShare.Client.Data.Interfaces;
using LibShare.Client.Data.Interfaces.IRepositories;
using LibShare.Client.Data.Repositories;
using LibShare.Client.Data.Services;
using LibShare.Client.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace LibShare.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            #region Resources.Messages
            builder.Services.AddSingleton(new ResourceManager("LibShare.Client.Data.Resources.Messages", Assembly.GetExecutingAssembly()));
            #endregion

            #region BlazorFileReader
            builder.Services.AddFileReaderService(options => options.InitializeOnFirstCall = true);
            #endregion

            builder.Services.AddSingleton(new SearchState());
            #region Interfaces
            builder.Services.AddTransient<IAccountRepository, AccountRepository>();
            builder.Services.AddTransient<IHttpService, HttpService>();
            builder.Services.AddTransient<ILibraryService, LibraryService>();
            #endregion

            await builder.Build().RunAsync();
        }
    }
}
