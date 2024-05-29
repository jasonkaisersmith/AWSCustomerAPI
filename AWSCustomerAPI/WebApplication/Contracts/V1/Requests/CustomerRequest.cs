
namespace AWSCustomerAPI.Contracts.V1.Requests
{
    public class CustomerRequest: CustomerEntityBaseRequest
    {
        public DateTime? LastUpdate { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }


        public CustomerRequest()
        {

        }
    }
}
