using LibShare.Client.ApiModels;
using LibShare.Client.Data.Interfaces;

namespace LibShare.Client.Data.Services
{
    public class AuthService : IAuthService
    {
        public JWTApiModel Login(LoginApiModel model)
        {
            return new JWTApiModel { RefreshToken = "some", Token = "some" };
        }
    }
}
