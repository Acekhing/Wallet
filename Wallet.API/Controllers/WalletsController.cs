using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
using Wallet.Application.Commands.WalletCommands;
using Wallet.Application.DTOs;
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
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDTO dto)
        {
            return await _mediator.SendCommand(this, new CreateWalletCommand { DTO = dto });
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletDTO dto)
        {
            return await _mediator.SendCommand(this, new UpdateWalletCommand { DTO = dto});
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWallet([FromBody] DeleteWalletCommand command)
        {
            return await _mediator.SendCommand(this, command);
        }
    }
}
