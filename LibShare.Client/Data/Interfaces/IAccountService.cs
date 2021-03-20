using LibShare.Client.Data.ApiModels;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface IAccountService : ICrudService<UserApiModel, string>
    {
        Task<TokenApiModel> LoginUserAsync(LoginApiModel model);
        Task<TokenApiModel> RegisterUserAsync(RegisterApiModel model);
        Task<TokenApiModel> RefreshTokenAsync(TokenApiModel model);
        Task LogoutAsync();
        Task<MessageApiModel> DeleteMe();
    }
}
