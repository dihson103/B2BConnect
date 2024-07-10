using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.ParticipationRepositoryTest;
public class IsBusinessRequestedAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IParticipationRepository _participationRepository;
    public IsBusinessRequestedAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _participationRepository = new ParticipationRepository(_context);
    }

    [Fact]
    public async Task ReturnFalse_WhenBusinessIdNotFound()
    {
        var businessId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var participation = Participation.Create(businessId, eventId, true);
        _context.Participations.Add(participation);
        _context.SaveChanges();

        var isBusinessRequested = await _participationRepository.IsBusinessRequestedAsync(Guid.NewGuid(), eventId);

        Assert.False(isBusinessRequested);
    }

    [Fact]
    public async Task ReturnFalse_WhenEventIdNotFound()
    {
        var businessId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var participation = Participation.Create(businessId, eventId, true);
        _context.Participations.Add(participation);
        _context.SaveChanges();

        var isBusinessRequested = await _participationRepository.IsBusinessRequestedAsync(businessId, Guid.NewGuid());

        Assert.False(isBusinessRequested);
    }

    [Fact]
    public async Task ReturnFalse_WhenEventIdAndBusinessIdNotFound()
    {
        var businessId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var participation = Participation.Create(businessId, eventId, true);
        _context.Participations.Add(participation);
        _context.SaveChanges();

        var isBusinessRequested = await _participationRepository.IsBusinessRequestedAsync(Guid.NewGuid(), Guid.NewGuid());

        Assert.False(isBusinessRequested);
    }

    [Fact]
    public async Task ReturnTrue_WhenFound()
    {
        var businessId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var participation = Participation.Create(businessId, eventId, true);
        _context.Participations.Add(participation);
        _context.SaveChanges();

        var isBusinessRequested = await _participationRepository.IsBusinessRequestedAsync(businessId, eventId);

        Assert.True(isBusinessRequested);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
