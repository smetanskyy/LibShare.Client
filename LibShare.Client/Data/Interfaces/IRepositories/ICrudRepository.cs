using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces.IRepositories
{
    public interface ICrudRepository<TypeModel, TypeId> where TypeModel : class
    {
        Task<TypeModel> CreateAsync(TypeModel item);
        Task<TypeModel> GetByIdAsync(TypeId id);
        Task<TypeModel> UpdateAsync(TypeModel item);
        Task<TypeModel> DeleteAsync(TypeId id, string deletionReason);
    }
}
