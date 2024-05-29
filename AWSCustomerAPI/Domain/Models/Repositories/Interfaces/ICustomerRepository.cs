using AWSCustomerAPI.Domain.Models.Repositories.Interfaces;
using AWSCustomerAPI.Models.Repositories;

namespace AWSCustomerAPI.Models.Repositories
{
    public interface ICustomerRepository :
        IRepositoryCreateDelete<Customer>,
        IRepositoryUpdate<Customer>,
        IRepositoryGetById<Customer>
    {

    }
}
