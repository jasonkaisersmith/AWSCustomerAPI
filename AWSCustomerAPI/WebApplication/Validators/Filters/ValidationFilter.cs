using AWSCustomerAPI.Validators.ErrorResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Validators.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before controller
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var suberror in error.Value!)
                    {
                        if (suberror != null)
                        {
                            var errorModel = new ErrorModel
                            {
                                FieldName = error.Key,
                                Message = suberror
                            };

                            errorResponse.Errors.Add(errorModel);
                        }
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
            }

            await next(); //Call next in pipeline

            //after controller
            //Not currently used
        }
    }
}
