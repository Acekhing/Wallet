using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
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

        public AccountSchemesController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _mediator.SendQuery(this, new GetAllAccountSchemesQuery());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountShemeCommand command) 
        {
            return await _mediator.SendCommand(this, command);
        }


        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateeAccountShemeCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAccountShemeCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }

    }
}
