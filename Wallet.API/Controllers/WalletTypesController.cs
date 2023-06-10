using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
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

        public WalletTypesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await _mediator.HandleQuery(this, new GetAllWalletTypeQuery());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalletTypeCommand command)
        {
            return await _mediator.HandleCommand(this, command);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateWalletTypeCommand command)
        {
            return await _mediator.HandleCommand(this, command);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteWalletTypeCommand command)
        {
            return await _mediator.HandleCommand(this, command);
        }
    }
}
