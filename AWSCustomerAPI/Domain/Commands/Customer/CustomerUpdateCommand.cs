using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Commands
{
    public class CustomerUpdateCommand :
        CustomerEntityBaseRequest,
        IRequest<SingleResponseWrapper>
    {
        public DateTime? LastUpdate { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }


        public CustomerUpdateCommand()
        {

        }
    }
}
