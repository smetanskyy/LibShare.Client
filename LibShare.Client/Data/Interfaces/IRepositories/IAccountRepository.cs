using LibShare.Client.Data.ApiModels;
using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces.IRepositories
{
    interface IAccountRepository : ICrudRepository<UserApiModel, string>
    {
        Task<TokenApiModel> LoginUserAsync(LoginApiModel model);
    }
}
