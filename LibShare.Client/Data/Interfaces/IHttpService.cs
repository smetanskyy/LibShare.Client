using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface IHttpService
    {
        Task<TResponse> Get<TResponse>(string url);
        Task<TResponse> Post<TRequest, TResponse>(string url, TRequest data);
        Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest data);
    }
}
