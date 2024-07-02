using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                option => option.MaxBatchSize(100)
            );

        });

        services.AddScoped<IUnitOfWork>(option => option.GetRequiredService<AppDbContext>());
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}
