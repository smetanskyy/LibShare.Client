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

        public static async void HideRecaptchaIcon(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("hideRecaptcha");
        }

        public static async ValueTask RenderJS(this IJSRuntime jSRuntime)
        {
            await jSRuntime.InvokeVoidAsync("renderJS");
        }
    }
}
