using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AWSCustomerAPI.PipelineBehaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;
        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }      

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            //DumpRequest(request);

            TResponse? response = default(TResponse);
            response = await next();
            _logger.LogDebug($"Sucessfully Handled {typeof(TRequest).Name}");
            /*
            try
            {
                response = await next();
                _logger.LogDebug($"Sucessfully Handled {typeof(TResponse).Name}");
            }
            catch(DbException ex)
            {
                _logger.LogDebug($"DbException {typeof(TResponse).Name}. {ex.ErrorCode} {ex.Message} {ex.StackTrace}");
            }
            //Response*/

            return response;
        }

        private void DumpRequest(TRequest request)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            Type myType = request.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop?.GetValue(request, null);
                _logger.LogInformation("{Property} : {@Value}", prop?.Name, propValue);
            }
        }
    }
}
