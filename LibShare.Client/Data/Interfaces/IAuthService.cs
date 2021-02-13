using LibShare.Client.ApiModels;

namespace LibShare.Client.Data.Interfaces
{
    public interface IAuthService
    {
        JWTApiModel Login(LoginApiModel model);
    }
}
