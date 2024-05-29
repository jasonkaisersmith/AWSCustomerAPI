
namespace AWSCustomerAPI.Contracts.V1.Responses
{
    public class CustomerResponse : CustomerEntityBaseResponse
    {
        public DateTime? LastUpdate { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }

        public CustomerResponse()
        {

        }
    }
}
