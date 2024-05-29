namespace AWSCustomerAPI.Domain.Models.Repositories.Interfaces
{
    public interface IRepositoryGetById<T>
    {
        Task<T?> GetById(Guid id);
    }
}
