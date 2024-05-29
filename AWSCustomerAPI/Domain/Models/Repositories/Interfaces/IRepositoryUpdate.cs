using System.Threading.Tasks;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface IRepositoryUpdate<T>
    {
        Task<bool> Update(T entity);
    }
}