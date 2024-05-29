using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface ICustomerIdQuery<T>
    {
        Task<IEnumerable<T>> GetByCustomerId(Guid id);

    }
}
