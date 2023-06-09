using System.Threading.Tasks;
using Wallet.Application.DTOs.AuthModels;
using Wallet.Application.Responses;

namespace Wallet.Application.Contracts.Auth
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginDto dto);
        public Task<BaseReponse> RegisterAsync(RegisterDto dto);
    }
}
