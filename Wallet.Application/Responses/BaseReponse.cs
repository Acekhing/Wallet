using System.Collections.Generic;

namespace Wallet.Application.Responses
{
    public class BaseReponse
    {
        public string Id { get; set; } = string.Empty;
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
