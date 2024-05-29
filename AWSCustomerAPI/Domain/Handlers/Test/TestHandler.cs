using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Responses;
using AWSCustomerAPI.Models;
using AWSCustomerAPI.Models.Repositories;
using AWSCustomerAPI.Queries;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Handlers
{
    public class TestHandler : IRequestHandler<TestQuery, SingleResponseWrapper>
    {
        private readonly ILogger<TestHandler> _logger;

        public TestHandler(ILogger<TestHandler> logger)
        {
            this._logger = logger;
        }

        public async Task<SingleResponseWrapper> Handle(TestQuery query, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Trace, nameof(TestHandler));
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var wrapper = new CustomerResponse() { Firstname = "Test", Id = Guid.NewGuid(), Surname = "Tester", LastUpdate = DateTime.Now };
            return new SingleResponseWrapper(wrapper);
        }
    }
}
