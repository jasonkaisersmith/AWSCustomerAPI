using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Contracts.V1.Responses;
using AWSCustomerAPI.Models;
using AWSCustomerAPI.Models.Repositories;
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
    public class CustomerCreateHandler : IRequestHandler<CustomerCreateCommand, SingleResponseWrapper>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerCreateHandler> _logger;

        public CustomerCreateHandler(ILogger<CustomerCreateHandler> logger, ICustomerRepository repository)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<SingleResponseWrapper> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Trace, typeof(CustomerCreateHandler).Name);
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id == Guid.Empty) 
                request.Id = Guid.NewGuid();
            if (request.LastUpdate == null)
                request.LastUpdate = DateTime.UtcNow;

            var toCreate = request.Adapt<Customer>();

            bool createdOK = await _repository.Create(toCreate);
            var customer = await _repository.GetById(request.Id);

            var wrapper = customer.Adapt<CustomerResponse>();
            return new SingleResponseWrapper(wrapper);
        }
    }
}
