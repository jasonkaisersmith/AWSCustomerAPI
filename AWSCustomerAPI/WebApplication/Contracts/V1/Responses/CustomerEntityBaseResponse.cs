namespace AWSCustomerAPI.Contracts.V1.Responses
{
    public abstract class CustomerEntityBaseResponse : BaseResponse
    {
        public Guid Id { get; set; }

        public CustomerEntityBaseResponse()
        {

        }

    }
}