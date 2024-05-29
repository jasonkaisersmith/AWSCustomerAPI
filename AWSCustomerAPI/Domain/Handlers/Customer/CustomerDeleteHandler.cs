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
    public class CustomerDeleteHandler : IRequestHandler<CustomerDeleteCommand, SingleResponseWrapper>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger<CustomerDeleteHandler> _logger;

        public CustomerDeleteHandler(ILogger<CustomerDeleteHandler> logger, ICustomerRepository repository)
        {
            this._repository = repository;
            this._logger = logger;
        }

        public async Task<SingleResponseWrapper> Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            _logger.Log(LogLevel.Trace, typeof(CustomerDeleteHandler).Name);

            var worked = await _repository.Delete(request.Id);
            return new SingleResponseWrapper();

        }
    }
}
