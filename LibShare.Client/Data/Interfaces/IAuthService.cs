using LibShare.Client.Data.ApiModels;

namespace LibShare.Client.Data.Interfaces
{
    public interface IAuthService
    {
        TokenApiModel Login(LoginApiModel model);
    }
}
