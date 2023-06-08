using System.Collections.Generic;
using Wallet.Application.Responses;

namespace Wallet.Application.Extensions
{
    public static class BaseResponseExtensions
    {
        public static BaseReponse Created(this BaseReponse response, string id, string message)
        {
            response.Id = id;
            response.Message = message;
            response.Success = true;

            return response;
        }

        public static BaseReponse Updated(this BaseReponse response, string id, string message)
        {
            response.Id = id;
            response.Message = message;
            response.Success = true;

            return response;
        }

        public static BaseReponse Failed(this BaseReponse response, string message)
        {
            response.Message = message;
            response.Success = false;

            return response;
        }
    }
}
