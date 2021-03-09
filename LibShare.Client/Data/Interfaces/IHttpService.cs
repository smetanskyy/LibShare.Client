using LibShare.Client.Helpers;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
    }
}
