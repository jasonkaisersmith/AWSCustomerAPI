using AWSCustomerAPI.Commands;
using AWSCustomerAPI.Contracts.V1.Responses;
using AWSCustomerAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator mediator;

        public TestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerResponse>> Test()
        {
            var result = await this.mediator.Send(new TestQuery());

            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success =>
                    Ok(result?.ResponseObject),
                _ => BadRequest(result?.ErrorMessage)
            };
        }
        
    }
}
