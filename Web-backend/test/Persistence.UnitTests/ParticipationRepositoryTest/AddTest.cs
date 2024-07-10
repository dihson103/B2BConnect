using Application.Abstractions.Data;
using Contract.Services.Business.Share;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.ParticipationRepositoryTest;
public class AddTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IParticipationRepository _participationRepository;
    public AddTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _participationRepository = new ParticipationRepository(_context);
    }

    [Fact]
    public async Task ShouldAddToDB()
    {
        var businessId = Guid.NewGuid();
        var eventId = Guid.NewGuid();
        var participation = Participation.Create(businessId, eventId, true);

        _participationRepository.Add(participation);
        await _context.SaveChangesAsync();

        var p = _context.Participations.First();

        Assert.NotNull(p);
        Assert.Equal(p.BusinessId, businessId);
        Assert.Equal(p.EventId, eventId);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
