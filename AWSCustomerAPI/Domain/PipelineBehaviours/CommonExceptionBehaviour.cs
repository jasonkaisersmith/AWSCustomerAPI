
using Amazon.DynamoDBv2.Model;
using AWSCustomerAPI.Contracts.V1;
using AWSCustomerAPI.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;

namespace AWSCustomerAPI.PipelineBehaviours
{

    public class CommonExceptionBehaviour
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CommonExceptionBehaviour> _logger;

        public CommonExceptionBehaviour(RequestDelegate next, ILogger<CommonExceptionBehaviour> logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError("Application exception: {message}", exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception switch
            {
                AWSCustomerException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

            return context.Response
                .WriteAsync(JsonConvert.SerializeObject(new SingleResponseWrapper().SetError(exception)));
        }
    }
}
