using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using StackExchange.Redis;
using System;
using System.Reflection;
using System.Text;
using Wallet.Application.Configs;
using Wallet.Application.Contracts.Auth;
using Wallet.Application.Contracts.Persistence;
using Wallet.Infrastructure.Persistence.Caching;
using Wallet.Infrastructure.Persistence.Data;
using Wallet.Infrastructure.Persistence.Repositories;

namespace Wallet.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepositry<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
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

        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(configuration.GetValue<string>("RedisConnection")));
            services.AddScoped<IDatabase>(sp =>
            {
                var connection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection"));
                return connection.GetDatabase();
            });
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }

        public static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            // Get runing environment
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //var configurationRoot = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            //    .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(ConfigureElasticSearch(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
               .CreateLogger();

            return services;
        }

        private static ElasticsearchSinkOptions ConfigureElasticSearch(IConfiguration configuration, string environment)
        {
            var assemblyname = Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-");
            var indexformat = $"{assemblyname}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}";

            return new ElasticsearchSinkOptions(new Uri(configuration.GetConnectionString("ElasticConnection")))
            {
                AutoRegisterTemplate = true,
                IndexFormat = indexformat,
                NumberOfReplicas = 1,
            };
        }
    }
}
