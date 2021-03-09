using LibShare.Client.Data.ApiModels;
using LibShare.Client.Data.Interfaces;

namespace LibShare.Client.Data.Services
{
    public class AuthService : IAuthService
    {
        public TokenApiModel Login(LoginApiModel model)
        {
            return new TokenApiModel { RefreshToken = "some", Token = "some" };
        }
    }
}
