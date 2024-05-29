using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Contracts.V1.Requests
{
    public abstract class CustomerEntityBaseRequest
    {
        public Guid Id { get; set; }

        public CustomerEntityBaseRequest()
        {

        }
    }
}
