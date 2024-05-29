using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface IRepositoryGetByIds<T>
    {
        Task<IEnumerable<T>> GetByIds(IEnumerable<long> idList);
    }
}