namespace AWSCustomerAPI.Domain.Exceptions
{
    public abstract class AWSCustomerException : Exception
    {
        public AWSCustomerException(string message) : base(message) { }
        public AWSCustomerException(string message, Exception inner) : base(message, inner) { }

    }
}
