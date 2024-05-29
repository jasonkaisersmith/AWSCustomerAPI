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
    public class CustomerGetByIdHandler : IRequestHandler<CustomerGetByIdQuery, SingleResponseWrapper>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerGetByIdHandler> _logger;

        public CustomerGetByIdHandler(ILogger<CustomerGetByIdHandler> logger, ICustomerRepository repository)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<SingleResponseWrapper> Handle(CustomerGetByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Trace, nameof(CustomerGetByIdHandler));
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var customer = await _repository.GetById(query.Id);

            var wrapper = customer.Adapt<CustomerResponse>();
            return new SingleResponseWrapper(wrapper);
        }
    }
}
