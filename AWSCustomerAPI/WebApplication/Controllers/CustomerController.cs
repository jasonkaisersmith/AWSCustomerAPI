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
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAll()
        {
            var result = await this.mediator.Send(new CustomerGetAllQuery());
            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success =>
                    Ok(result.ResponseObject),
                _ => BadRequest(result.ErrorMessage),
            };
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetById(Guid id)
        {
            var result = await this.mediator.Send(new CustomerGetByIdQuery(id));

            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success =>
                    Ok(result?.ResponseObject),
                _ => BadRequest(result?.ErrorMessage)
            };
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponse>> Create([FromBody] CustomerCreateCommand command)
        {
            var result = await this.mediator.Send(command);
            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success =>
                    CreatedAtAction(nameof(Create), new { id = (result?.ResponseObject as CustomerEntityBaseResponse)?.Id }, result?.ResponseObject),
                _ => BadRequest(result?.ErrorMessage),
            };
        }

        [HttpPut]
        public async Task<ActionResult<CustomerResponse>> Update([FromBody] CustomerUpdateCommand command)
        {
            var result = await this.mediator.Send(command);
            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success => Ok(result.ResponseObject),
                _ => Problem(result?.ErrorMessage),
            };
        }


        [HttpDelete]
        public async Task<ActionResult<CustomerResponse>> Delete(Guid id)
        {
            CustomerDeleteCommand command = new() { Id = id };
            var result = await this.mediator.Send(command);

            return result.ResponseState switch
            {
                Contracts.V1.ResponseState.Success => Ok(result.ResponseObject),
                _ => Problem(result.ErrorMessage),
            };
        }

    }
}
