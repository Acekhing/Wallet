using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Commands.AccountSchemeCommands;
using Wallet.Application.Commands.WalletTypeCommands;
using Wallet.Application.Queries.AccountSchemeQueries;
using Wallet.Application.Queries.WalletQueries;
using Wallet.Application.Queries.WalletTypeQueries;

namespace Wallet.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWallets()
        {
            var results = await _mediator.Send(new GetAllWalletsQuery());

            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountSchemes()
        {
            var results = await _mediator.Send(new GetAllAccountSchemesQuery());

            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletTypes()
        {
            var results = await _mediator.Send(new GetAllWalletTypeQuery());;

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWalletType([FromBody] CreateWalletTypeCommand command)
        {
            var results = await _mediator.Send(command);

            if(results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountScheme([FromBody] CreateAccountShemeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateWalletType([FromBody] UpdateWalletTypeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAccountScheme([FromBody] UpdateeAccountShemeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }
    }
}
