using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Requests;
using FluentValidation;

namespace AWSCustomerAPI.Validators
{
    public class DeleteCustomerValidator : AbstractValidator<CustomerDeleteCommand>
    {
        public DeleteCustomerValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
