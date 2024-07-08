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
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddSingleton<IJwtService>(new JwtService(configuration));

        services.AddStackExchangeRedisCache(options =>
        {
            var connection = configuration.GetConnectionString("Redis");
            options.Configuration = connection;

        });
        services.AddScoped<IRedisService, RedisService>();
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
