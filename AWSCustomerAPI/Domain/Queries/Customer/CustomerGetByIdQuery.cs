using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Queries
{
    public class CustomerGetByIdQuery : IRequest<SingleResponseWrapper>
    {
        public Guid Id { get; set; }
        public CustomerGetByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
