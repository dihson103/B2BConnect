using Application.Abstractions.Services;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
    {
        services.AddSingleton<IPasswordService, PasswordService>();

        services.AddStackExchangeRedisCache(options =>
        {
            var connection = configuration.GetConnectionString("Redis");
            options.Configuration = connection;

        });
        services.AddScoped<IRedisService, RedisService>();
        services.AddSingleton<IFileService, FileService>();
        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}
