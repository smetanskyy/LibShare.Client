﻿using LibShare.Client.Data.ApiModels;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    interface IAccountService : ICrudService<UserApiModel, string>
    {
        Task<TokenApiModel> LoginUserAsync(LoginApiModel model);
        Task<TokenApiModel> RegisterUserAsync(RegisterApiModel model);
    }
}
