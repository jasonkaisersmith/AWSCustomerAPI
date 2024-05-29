using AWSCustomerAPI.Contracts.V1.Requests;
using AWSCustomerAPI.Contracts.V1.Responses;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Contracts.V1
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseState { Success, Error }
    public abstract class ResponseWrapperBase
    {
        public object? RequestObject { get; private set; }
        public ResponseState ResponseState { get; set; }

        public string ErrorMessage
        {
            get
            {
                return this.ErrorMessages?[0] ?? "";
            }
        }
        public List<string> ErrorMessages { get; set; }
        public Exception? Exception { get; set; }

        public ResponseWrapperBase()
        {
            this.ResponseState = ResponseState.Success;
            this.ErrorMessages = new List<string>();
        }

        /// <summary>
        /// Initialise ResponseWrapper with Error state and error message
        /// </summary>
        /// <param name="errormessage"></param>
        public ResponseWrapperBase(string errormessage) : this()
        {
            this.ResponseState = ResponseState.Error;
            this.ErrorMessages.Add(errormessage);
        }

        public ResponseWrapperBase SetError<TException>(TException exception, object? request = null) where TException : Exception
        {
            this.RequestObject = request;
            this.Exception = exception;
            this.ResponseState = ResponseState.Error;

            this.ErrorMessages.Add(exception.Message);
            if (exception.InnerException != null)
                this.ErrorMessages.Add(exception.InnerException.Message);

            return this;
        }
    }
}
