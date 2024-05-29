using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Commands
{
    public class CustomerDeleteCommand : IRequest<SingleResponseWrapper>
    {
        public Guid Id { get; set; }

        public CustomerDeleteCommand()
        {

        }
    }
}
