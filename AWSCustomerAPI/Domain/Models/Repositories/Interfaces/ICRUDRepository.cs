using AWSCustomerAPI.Domain.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface ICRUDRepository<T> :
        IRepositoryCreateDelete<T>,
        IRepositoryUpdate<T>,
        IRepositoryGetById<T>
    {
        Task<IEnumerable<T>> GetAll();

    }
}
