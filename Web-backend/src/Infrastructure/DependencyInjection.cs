using System.Text;
using Application.Abstractions.Services;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddSingleton<IJwtService>(new JwtService(configuration));

        services.AddStackExchangeRedisCache(options =>
        {
            var connection = configuration.GetConnectionString("Redis");
            options.Configuration = connection;

        });
        services.AddScoped<IRedisService, RedisService>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = configuration.GetSection("jwtOption").Get<JwtOptions>();
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions?.Issuer,
                    ValidAudience = jwtOptions?.Audience,
                    IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                };
            });
        services.AddAuthorization();
        services.AddAuthorizationBuilder()
            .AddPolicy("require-admin", policy => policy.RequireClaim("isAdmin", "True"));

        return services;
    }
}
