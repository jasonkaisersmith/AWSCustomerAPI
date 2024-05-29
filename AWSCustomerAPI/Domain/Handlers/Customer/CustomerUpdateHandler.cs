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
    public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateCommand, SingleResponseWrapper>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerUpdateHandler> _logger;

        public CustomerUpdateHandler(ILogger<CustomerUpdateHandler> logger, ICustomerRepository repository)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<SingleResponseWrapper> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Trace, typeof(CustomerUpdateHandler).Name);

            var toUpdate = request.Adapt<Customer>();
            toUpdate.LastUpdate = DateTime.Now;

            var rows = await _repository.Update(toUpdate);
            var cust = await _repository.GetById(new Guid(toUpdate.Id));
           

            var wrapper = cust.Adapt<CustomerResponse>();
            return new SingleResponseWrapper(wrapper);
        }
    }
}
