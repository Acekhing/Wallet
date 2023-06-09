using System.Collections.Generic;
using Wallet.Application.Responses;

namespace Wallet.Application.Extensions
{
    public static class BaseResponseExtensions
    {
        public static BaseReponse Success(this BaseReponse response, string? id = null, string? message = null)
        {
            response.Id = id;
            response.Message = message;
            response.Success = true;

            return response;
        }

        public static BaseReponse Failed(this BaseReponse response, string action, string message)
        {
            response.Message = $"{action} failed";
            response.Success = false;
            response.Errors = new List<string> { message };

            return response;
        }
    }
}
