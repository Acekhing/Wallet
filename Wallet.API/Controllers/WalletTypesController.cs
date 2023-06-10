using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Commands.WalletTypeCommands;
using Wallet.Application.Queries.WalletTypeQueries;

namespace Wallet.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? search = null)
        {
            var results = await _mediator.Send(new GetAllWalletTypeQuery { Search = search }); ;

            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalletTypeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateWalletTypeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteWalletTypeCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }
    }
}
