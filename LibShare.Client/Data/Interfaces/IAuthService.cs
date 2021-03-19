using LibShare.Client.Data.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginApiModel model);
        Task Register(RegisterApiModel model);
        Task Logout();
        Task RefreshToken();
        Task UpdateToken(TokenApiModel model);
    }
}
