using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventIndustryRepositoryTest;
public class GetByEventIdAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventIndustryRepository _eventIndustryRepository;
    public GetByEventIdAsyncTest()
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
                null,
                null
                ), "099999"
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
                ), "09977"
            );
        _context.Events.Add(newEvent1);
        _context.Events.Add(newEvent);
        _context.Industries.Add(idustry_1);
        _context.Industries.Add(idustry_2);
        _context.SaveChanges();

        var eventIndustries = new List<EventIndustry>()
        {
            EventIndustry.Create(idustry_1.Id, newEvent.Id),
            EventIndustry.Create(idustry_2.Id, newEvent.Id),
            EventIndustry.Create(idustry_1.Id, newEvent1.Id),
        };

        _context.EventIndustries.AddRange(eventIndustries);
        _context.SaveChanges();
    }

    [Fact]
    public async Task ReturnEmpty_WhenNotFound()
    {
        var result = await _eventIndustryRepository.GetByEventIdAsync(Guid.NewGuid());
        Assert.Equal(0, result.Count);
    }

    [Fact]
    public async Task ReturnNotNull_WhenFound()
    {
        var idustry = Industry.Create("1");
        var newEvent = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                null,
                null
                ), "09878"
            );
        var eventIndustry = EventIndustry.Create(idustry.Id, newEvent.Id);
        _context.Events.Add(newEvent);
        _context.Industries.Add(idustry);
        _context.EventIndustries.Add(eventIndustry);
        _context.SaveChanges();

        var result = await _eventIndustryRepository.GetByEventIdAsync(newEvent.Id);
        Assert.NotNull(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
