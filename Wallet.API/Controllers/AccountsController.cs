using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Commands.AuthCommands;
using Wallet.Application.DTOs.AuthModels;

namespace Wallet.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var results = await _mediator.Send(new LoginCommand { LoginDto = dto});

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var results = await _mediator.Send(new RegisterCommand { RegisterDto = dto });

            return Ok(results);
        }
    }
}
