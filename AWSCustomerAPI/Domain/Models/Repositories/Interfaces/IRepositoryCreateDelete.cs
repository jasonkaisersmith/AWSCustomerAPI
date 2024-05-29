using System.Threading.Tasks;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface IRepositoryCreateDelete<T>
    {
        Task<bool> Create(T entity);
        Task<bool> Delete(Guid id);
    }
}