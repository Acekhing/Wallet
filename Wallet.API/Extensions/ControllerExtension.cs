using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wallet.Application.Responses;

namespace Wallet.API.Extensions
{
    public static class ControllerExtension
    {
        public static async Task<IActionResult> HandleCommand<TCommand>
        (
            this IMediator _mediator, ControllerBase controller ,TCommand command
        ) 
            where TCommand : IRequest<BaseReponse>
        {
            var results = await _mediator.Send(command);

            if (results.Success)
                return controller.Ok(results);

            return controller.BadRequest(results.Errors);
        }

        public static async Task<IActionResult> HandleQuery<TCommand>
        (
             this IMediator _mediator, ControllerBase controller, TCommand command
        )
            where TCommand : IRequest<QueryResponse>
        {
            var results = await _mediator.Send(command);

            if (results.Success)
                return controller.Ok(results.Data);

            return controller.BadRequest(results.Errors);
        }
    }
}
