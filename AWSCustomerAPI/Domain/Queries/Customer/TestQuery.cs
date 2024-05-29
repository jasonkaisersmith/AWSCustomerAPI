using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Queries
{
    public class TestQuery : IRequest<SingleResponseWrapper>
    {
        public TestQuery()
        {

        }
    }
}
