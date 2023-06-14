using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
using Wallet.Application.Commands.WalletTypeCommands;
using Wallet.Application.DTOs;
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
            return await _mediator.SendQuery(this, new GetAllWalletTypeQuery());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAccountTypeDTO dto)
        {
            return await _mediator.SendCommand(this, new CreateAccountTypeCommand { DTO = dto });
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateAccountTypeDTO dto)
        {
            return await _mediator.SendCommand(this, new UpdateAccountTypeCommand { DTO = dto });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return await _mediator.SendCommand(this, new DeleteAccountTypeCommand { Id = id });
        }
    }
}
