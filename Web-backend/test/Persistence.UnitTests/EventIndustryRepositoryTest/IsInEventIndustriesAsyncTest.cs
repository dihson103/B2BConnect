using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventIndustryRepositoryTest;
public class IsInEventIndustriesAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventIndustryRepository _eventIndustryRepository;
    public IsInEventIndustriesAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventIndustryRepository = new EventIndustryRepository(_context);

        InitDb();
    }
    private void InitDb()
    {
        var idustry_1 = Industry.Create("1");
        var idustry_2 = Industry.Create("1");
        var newEvent = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                null, null
                ), "09899"
            );
        var newEvent1 = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                null,
                null
                ), "09899"
            );
        _context.Events.Add(newEvent1);
        _context.Events.Add(newEvent);
        _context.Industries.Add(idustry_1);
        _context.Industries.Add(idustry_2);
        _context.SaveChanges();

        var eventIndustries = new List<EventIndustry>()
        {
            EventIndustry.Create(newEvent.Id, idustry_1.Id),
            EventIndustry.Create(newEvent.Id, idustry_2.Id),
            EventIndustry.Create(newEvent1.Id, idustry_1.Id),
        };

        _context.EventIndustries.AddRange(eventIndustries);
        _context.SaveChanges();
    }

    [Fact]
    public async Task ReturnFalse_WhenEventIdNotFound()
    {
        var industryIds = _context.Industries.Select(i => i.Id).ToList();
        var eventId = Guid.NewGuid();

        var isInEventIndustries = await _eventIndustryRepository.IsInEventIndustriesAsync(industryIds, eventId);

        Assert.False(isInEventIndustries);
    }

    [Fact]
    public async Task ReturnFalse_WhenIndustryIdsNotFound()
    {
        var industryIds = new List<Guid> { Guid.NewGuid() };
        var eventId = _context.Events.First().Id;

        var isInEventIndustries = await _eventIndustryRepository.IsInEventIndustriesAsync(industryIds, eventId);

        Assert.False(isInEventIndustries);
    }

    [Fact] 
    public async Task ReturnTrue_WhenValid()
    {
        var industryIds = _context.Industries.Select(i => i.Id).ToList();
        var eventId = _context.Events.First().Id;

        var isInEventIndustries = await _eventIndustryRepository.IsInEventIndustriesAsync(industryIds, eventId);

        Assert.True(isInEventIndustries);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
