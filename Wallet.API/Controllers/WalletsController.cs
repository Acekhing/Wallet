using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
using Wallet.Application.Commands.WalletCommands;
using Wallet.Application.Queries.AccountSchemeQueries;
using Wallet.Application.Queries.WalletQueries;

namespace Wallet.API.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator) => _mediator = mediator;


        [HttpGet]
        public async Task<IActionResult> GetUserWallets(string userId)
        {
            return await _mediator.SendQuery(this, new GetAllUserWalletQuery { UserId = userId });

        }

        [HttpGet]
        public async Task<IActionResult> GetWalletById(string userId, string walletId)
        {
            return await _mediator.SendQuery(this, new GetWalletByIdQuery { UserId = userId, WalletId = walletId });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            return await _mediator.SendQuery(this, new GetAllWalletsQuery());
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWallet([FromBody] DeleteWalletCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }
    }
}
