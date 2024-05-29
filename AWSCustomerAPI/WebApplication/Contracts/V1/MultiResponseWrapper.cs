using AWSCustomerAPI.Contracts.V1.Requests;
using AWSCustomerAPI.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Contracts.V1
{
    public class MultiResponseWrapper : ResponseWrapperBase
    {
        public IEnumerable<CustomerEntityBaseResponse>? ResponseObject { get; }

        private MultiResponseWrapper() { }

        public MultiResponseWrapper(IEnumerable<CustomerEntityBaseResponse> resp) : base()
        {
            this.ResponseObject = resp;
        }

        /// <summary>
        /// Initialise ResponseWrapper with Error state and error message
        /// </summary>
        /// <param name="errormessage"></param>
        public MultiResponseWrapper(string errormessage) : base(errormessage)
        {

        }
    }
}
