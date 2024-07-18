using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventRepositoryTest;
public class GetByIdAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventRepository _eventRepository;
    public GetByIdAsyncTest()
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
                null, 
                null
                ), "0987"
            );
        var industry = Industry.Create("Industry");
        var eventIndustry = EventIndustry.Create(eventt.Id, industry.Id);

        _context.Events.Add(eventt);
        _context.Industries.Add(industry);
        _context.EventIndustries.Add(eventIndustry);
        _context.SaveChanges();
    }

    [Fact]
    public async Task ShouldReturnEvent_WhenIdExist()
    {
        var id = _context.Events.FirstOrDefault().Id;

        var result = await _eventRepository.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.NotNull(result.EventIndustries);
    }

    [Fact]
    public async Task ShouldReturnNull_WhenIdNotExist()
    {
        var id = Guid.NewGuid();

        var result = await _eventRepository.GetByIdAsync(id);

        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
