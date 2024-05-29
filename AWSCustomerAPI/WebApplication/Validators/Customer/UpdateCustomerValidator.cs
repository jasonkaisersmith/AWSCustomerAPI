using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Requests;
using FluentValidation;

namespace AWSCustomerAPI.Validators
{
    public class UpdateCustomerValidator : AbstractValidator<CustomerUpdateCommand>
    {
        public UpdateCustomerValidator()
        {
            //Last Update is allowed to be null/empty.  Will be created in handler.
            Include(new BaseRequestValidator());
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();

        }
    }
}
