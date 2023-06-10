using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using Wallet.Application.Exceptions;

namespace Wallet.API.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationCommandException ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleException(context, JsonConvert.SerializeObject(ex.Errors.Values));
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleException(context, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleException(context, ex.Message);
            }
        }

        private async Task HandleException(HttpContext context, string message)
        {
            if(!context.Response.HasStarted)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                var problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Internal server error",
                    Detail = message
                };

                string errorString = JsonConvert.SerializeObject(problem);

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(errorString);
            }
            else
            {
                 await context.Response.WriteAsync(message);
            }
        }
    }
}
