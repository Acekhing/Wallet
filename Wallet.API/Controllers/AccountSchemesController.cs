using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Commands.AccountSchemeCommands;
using Wallet.Application.Queries.AccountSchemeQueries;

namespace Wallet.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountSchemesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountSchemesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _mediator.Send(new GetAllAccountSchemesQuery());

            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountShemeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }


        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateeAccountShemeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountShemeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }
    }
}
