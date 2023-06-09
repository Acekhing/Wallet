﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Commands.WalletCommands;
using Wallet.Application.Queries.WalletQueries;

namespace Wallet.API.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetUserWallets(string userId)
        {
            var results = await _mediator.Send(new GetAllUserWalletQuery { UserId = userId });

            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletById(string userId, string walletId)
        {
            var results = await _mediator.Send(new GetWalletByIdQuery { UserId = userId, WalletId = walletId });

            return Ok(results);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllWallets()
        {
            var results = await _mediator.Send(new GetAllWalletsQuery());

            return Ok(results);
        }


        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateWallet([FromBody] UpdateWalletCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWallet([FromBody] DeleteWalletCommand command)
        {
            var results = await _mediator.Send(command);

            if (results.Success == true)
                return Ok(results);

            return BadRequest(results.Errors);
        }
    }
}
