using AWSCustomerAPI.Contracts.V1.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Contracts.V1
{
    public class SingleResponseWrapper : ResponseWrapperBase
    {
        public CustomerEntityBaseResponse? ResponseObject { get; }

        public SingleResponseWrapper(CustomerEntityBaseResponse resp) : base()
        {
            this.ResponseObject = resp;
        }

        public SingleResponseWrapper() : base() { }


        /// <summary>
        /// Initialise ResponseWrapper with Error state and error message
        /// </summary>
        /// <param name="errormessage"></param>
        public SingleResponseWrapper(string errormessage) : base(errormessage) { }

    }
}
