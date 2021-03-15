using System.Threading.Tasks;

namespace LibShare.Client.Data.Interfaces
{
    public interface ICrudService<TypeModel, TypeId> where TypeModel : class
    {
        Task<TypeModel> CreateAsync(TypeModel model);
        Task<TypeModel> GetByIdAsync(TypeId id);
        Task<TypeModel> UpdateAsync(TypeModel model);
        Task<TypeModel> DeleteAsync(TypeId id, string deletionReason);
    }
}
