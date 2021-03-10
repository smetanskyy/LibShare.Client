using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Data.Interfaces.IRepositories;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IHttpService _httpService;
        public AccountRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserApiModel> CreateAsync(UserApiModel model)
        {
            return await _httpService.Post<UserApiModel, UserApiModel>(ApiUrls.ClientEditProfile, model);
        }

        public Task<UserApiModel> DeleteAsync(string id, string deletionReason)
        {
            throw new NotImplementedException();
        }

        public Task<UserApiModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenApiModel> LoginUserAsync(LoginApiModel model)
        {
            return await _httpService.Post<LoginApiModel, TokenApiModel>(ApiUrls.LoginUrl, model);
        }

        public async Task<TokenApiModel> RegisterUserAsync(RegisterApiModel model)
        {
            return await _httpService.Post<RegisterApiModel, TokenApiModel>(ApiUrls.RegisterUrl, model);
        }

        public Task<UserApiModel> UpdateAsync(UserApiModel item)
        {
            throw new NotImplementedException();
        }
    }
}
