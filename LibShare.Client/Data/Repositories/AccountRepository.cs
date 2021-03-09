using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Constants;
using LibShare.Client.Data.Interfaces;
using LibShare.Client.Data.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<UserApiModel> CreateAsync(UserApiModel item)
        {
            var response = await _httpService.Post(ApiUrls.ClientEditProfile, item);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return (UserApiModel)response.Response;
        }

        public Task<UserApiModel> DeleteAsync(string id, string deletionReason)
        {
            throw new NotImplementedException();
        }

        public Task<UserApiModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TokenApiModel> LoginUserAsync(LoginApiModel model)
        {
            throw new NotImplementedException();
        }

        public Task<UserApiModel> UpdateAsync(UserApiModel item)
        {
            throw new NotImplementedException();
        }
    }
}
