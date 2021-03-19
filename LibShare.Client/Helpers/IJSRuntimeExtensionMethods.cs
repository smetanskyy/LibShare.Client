using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LibShare.Client.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<string> GetRecaptcha(this IJSRuntime jSRuntime, string actionName)
        {
            return await jSRuntime.InvokeAsync<string>("runRecaptcha", actionName);
        }

        public static async ValueTask ShowRecaptchaIcon(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("showRecaptcha");
        }

        public static async ValueTask LoadCropper(this IJSRuntime jSRuntime, object obj)
        {
            await jSRuntime.InvokeVoidAsync("cropperLoad", DotNetObjectReference.Create(obj));
        }

        public static async void HideRecaptchaIcon(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("hideRecaptcha");
        }

        public static async ValueTask RenderJS(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("renderJS");
        }

        public static async ValueTask<object> SetInLocalStorage(this IJSRuntime jSRuntime,
            string key, string content)
        {
            return await jSRuntime.InvokeAsync<object>("localStorage.setItem", key, content);
        }

        public static async ValueTask<string> GetFromLocalStorage(this IJSRuntime jSRuntime,
            string key)
        {
            return await jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }
        public static async ValueTask<string> RemoveFromLocalStorage(this IJSRuntime jSRuntime,
            string key)
        {
            return await jSRuntime.InvokeAsync<string>("localStorage.removeItem", key);
        }
    }
}
