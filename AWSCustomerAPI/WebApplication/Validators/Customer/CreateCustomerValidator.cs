using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Requests;
using FluentValidation;

namespace AWSCustomerAPI.Validators
{
    public class CreateCustomerValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CreateCustomerValidator()
        {
            //ID is allowed to be null/empty.  Will be created in handler.
            //Last Update is allowed to be null/empty.  Will be created in handler.
            RuleFor(x => x.Firstname).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            //Include(new BaseRequestValidator());
        }
    }
}
