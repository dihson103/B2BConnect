using Application.Abstractions.Data;
using Application.UseCases.Commands.Verifications.Create;
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
        services.AddScoped<IIndustryRepository, IndustryRepository>();
        services.AddScoped<IEventIndustryRepository, EventIndustryRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<ISectorRepository, SectorRepository>();
        services.AddScoped<IParticipationRepository, ParticipationRepository>();
        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IEventMediaRepository, EventMediaRepository>();
        services.AddScoped<IRepresentativeRepository, RepresentativeRepository>();
        services.AddScoped<IVerificationRepository, VerificationRepository>();


        return services;
    }
}
