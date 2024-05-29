using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Requests;
using AWSCustomerAPI.Contracts.V1.Responses;
using FluentValidation;

namespace AWSCustomerAPI.Validators
{
    public class BaseRequestValidator : AbstractValidator<CustomerEntityBaseRequest>
    {
        public BaseRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
