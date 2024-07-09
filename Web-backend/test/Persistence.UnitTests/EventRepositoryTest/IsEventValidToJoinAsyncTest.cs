using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventRepositoryTest;
public class IsEventValidToJoinAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventRepository _eventRepository;
    public IsEventValidToJoinAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventRepository = new EventRepository(_context);

        SeedingData();
    }
    private void SeedingData()
    {
        var eventt = Event.Create(
            new CreateEventCommand(
                "Name 1",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                "Image",
                null
                )
            );
        var industry = Industry.Create("Industry");
        var eventIndustry = EventIndustry.Create(eventt.Id, industry.Id);

        _context.Events.Add(eventt);
        _context.Industries.Add(industry);
        _context.EventIndustries.Add(eventIndustry);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

}
