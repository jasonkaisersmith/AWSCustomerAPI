
namespace AWSCustomerAPI.Models
{
    public interface ICustomer
    {
        string Id { get; set; }
        string? Firstname { get; set; }
        DateTime? LastUpdate { get; set; }
        string? Surname { get; set; }
    }
}