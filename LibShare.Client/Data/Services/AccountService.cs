using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpService _httpService;
        public AccountService(IHttpService httpService)
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

        public async Task<UserApiModel> GetByIdAsync(string id)
        {
            return await _httpService.Get<UserApiModel>(ApiUrls.GetUserById + id);
        }

        public async Task<TokenApiModel> LoginUserAsync(LoginApiModel model)
        {
            return await _httpService.Post<LoginApiModel, TokenApiModel>(ApiUrls.LoginUrl, model);
        }

        public async Task LogoutAsync()
        {
            await _httpService.Get<MessageApiModel>(ApiUrls.Logout);
        }

        public async Task<TokenApiModel> RefreshTokenAsync(TokenApiModel model)
        {
            return await _httpService.Post<TokenApiModel, TokenApiModel>(ApiUrls.RefreshTokenUrl, model);
        }

        public async Task<TokenApiModel> RegisterUserAsync(RegisterApiModel model)
        {
            return await _httpService.Post<RegisterApiModel, TokenApiModel>(ApiUrls.RegisterUrl, model);
        }

        public async Task<UserApiModel> UpdateAsync(UserApiModel model)
        {
            return await _httpService.Post<UserApiModel, UserApiModel>(ApiUrls.ClientEditProfile, model);
        }

        public async Task<MessageApiModel> DeleteMe(UserApiModel model)
        {
            return await _httpService.Put<UserApiModel, MessageApiModel>(ApiUrls.DeleteMeUrl, model);
        }
    }
}
