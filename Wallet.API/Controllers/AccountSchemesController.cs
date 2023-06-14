using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.API.Extensions;
using Wallet.Application.Commands.AccountSchemeCommands;
using Wallet.Application.DTOs;
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
        public async Task<IActionResult> Create([FromBody] CreateAccountSchemeDTO dto) 
        {
            return await _mediator.SendCommand(this, new CreateAccountShemeCommand { DTO = dto });
        }


        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateAccountSchemeDTO dto)
        {
            return await _mediator.SendCommand(this, new UpdateeAccountShemeCommand { DTO = dto });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            return await _mediator.SendCommand(this, new DeleteAccountShemeCommand { Id = id });
        }

    }
}
