using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using Wallet.API.Middlewares;

namespace Wallet.API.Extensions
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service)
        {
            // adds swagger service to service collections
            service.AddSwaggerGen(opt =>
            {

                opt.EnableAnnotations();

                // Add authorization header
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] an then your token in the next input below.\r\n\r\nExample: 'Bearer 1234etetrf'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme ="oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hubtel Wallet Api",
                    Version = "v1",
                    Description = "An API service to be used to manage a user's wallet on the Hubtel app",
                    Contact = new OpenApiContact
                    {
                        Email = "charlesannorblay@gmail.com",
                        Extensions = { },
                        Name = "Charles",
                        Url = new Uri("https://linkedin.com/in/Acekhing")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://hubtel.com/")
                    },
                    Extensions = { },
                    TermsOfService = new Uri("https://hubtel.com/")
                });

            });

            return service;
        }

        public static IServiceCollection AddException(this IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
            return services;
        }

        public static IServiceCollection AddRateLimiters(this IServiceCollection services)
        {
            // implemet rate limiting
            return services;
        }
    }
}
