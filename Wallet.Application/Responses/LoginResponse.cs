namespace Wallet.Application.Responses
{
    public class LoginResponse
    {
        public string UserId { get; set; } = string.Empty;
        public bool Sucess { get; set; } = false; 
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
