namespace AWSCustomerAPI.Contracts.V1.Responses
{
    public class BaseResponse
    {
        public DateTime RetrievedDateTimeUTC { get; } = DateTime.UtcNow;
    }
}