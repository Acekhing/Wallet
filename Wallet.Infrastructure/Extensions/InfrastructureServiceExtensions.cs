using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using System;
using Wallet.Application.Configs;
using Wallet.Application.Contracts.Auth;
using Wallet.Application.Contracts.Persistence;
using Wallet.Infrastructure.Persistence.Data;
using Wallet.Infrastructure.Persistence.Repositories;

namespace Wallet.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepositry<>));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddTransient<IWalletTypeRepository, WalletTypeRepository>();
            services.AddTransient<IWalletRepository, WalletRepository>();
            services.AddTransient<IAccountSchemeRepository, AccountSchemeRepository>();
            
            return services;
        }

        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoClient>(sc =>
            {
                return new MongoClient(configuration.GetConnectionString("MongoDbConnection"));
            });

            services.AddScoped(sc =>
            {
                var mongoClient = sc.GetRequiredService<IMongoClient>();
                return mongoClient.GetDatabase("hubtel");
            });

            return services;
        }
        
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<HubtelDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AuthDbConnection"));
            });

            services.AddIdentity<HubtelUser, IdentityRole>()
                .AddEntityFrameworkStores<HubtelDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });

            return services;
        }
    }
}
