using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions defaultJsonSerializerOptions => new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(url, stringContent);
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorMesssages.ServerNotFound);
            }

            return await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(url);
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorMesssages.ServerNotFound);
            }

            return await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest data)
        {
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsJsonAsync(url, data);
            }
            catch (Exception)
            {
                throw new ApplicationException(ErrorMesssages.ServerNotFound);
            }

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<MessageApiModel>();
                throw new ApplicationException(content.Message);
            }
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        private async Task<TResult> Deserialize<TResult>(HttpResponseMessage httpResponseMessage, JsonSerializerOptions options)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Deserialize<MessageApiModel>(responseString, options);
                throw new ApplicationException(error.Message);
            }

            return JsonSerializer.Deserialize<TResult>(responseString, options);
        }
    }
}
