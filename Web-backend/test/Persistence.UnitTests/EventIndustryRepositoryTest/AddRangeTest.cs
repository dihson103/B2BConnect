using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventIndustryRepositoryTest;
public class AddRangeTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventIndustryRepository _eventIndustryRepository;
    public AddRangeTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventIndustryRepository = new EventIndustryRepository(_context);
    }

    [Fact]
    public async Task AddSuccess()
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
                ), "097888"
            
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
                ),
            "097888"
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

        _eventIndustryRepository.AddRange(eventIndustries);
        await _context.SaveChangesAsync();

        var count = _context.EventIndustries.Count();

        Assert.Equal( 3, count );
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
