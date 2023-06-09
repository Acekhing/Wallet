using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Wallet.Application.Configs;
using Wallet.Application.Contracts.Auth;
using Wallet.Application.DTOs.AuthModels;
using Wallet.Application.Exceptions;
using Wallet.Application.Extensions;
using Wallet.Application.Responses;
using Wallet.Infrastructure.Persistence.Data;

namespace Wallet.Infrastructure.Persistence.Repositories
{
    public class AuthService : IAuthService
    {
        private readonly HubtelDbContext _db;
        private readonly UserManager<HubtelUser> _userManager;
        private readonly SignInManager<HubtelUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            HubtelDbContext db, 
            UserManager<HubtelUser> userManager, 
            SignInManager<HubtelUser> signInManager,
            IOptions<JwtSettings> options)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = options.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            
            if (user == null)
                throw new EntityNotFoundException($"User with {dto.Email} not found");

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, lockoutOnFailure: false);
            
            if (!result.Succeeded)
                throw new EntityNotFoundException($"Credentials for {dto.Email} are invalid");

            JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

            return new LoginResponse
            {
                UserId = user.Id,
                Message = "Login success",
                Sucess = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };
        }

        public async Task<BaseReponse> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByNameAsync(dto.UserName);

            if (existingUser != null)
                throw new Exception($"Username '{dto.UserName}' already exists.");

            var user = new HubtelUser
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
                EmailConfirmed = true
            };

            var existingEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (existingEmail == null)
            {
                var response = new BaseReponse();

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "user");
                    return response.Success(message:"Registration successfull");
                }
                else
                {
                    return response.Failed("Registration", $"{string.Join(", ", result.Errors.Select(x => x.Description))}");
                }
            }
            else
            {
                throw new Exception($"Email {dto.Email} already exists.");
            }
        }

        private async Task<JwtSecurityToken> GenerateToken(HubtelUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );

            return jwtSecurityToken;
        }
    }
}
