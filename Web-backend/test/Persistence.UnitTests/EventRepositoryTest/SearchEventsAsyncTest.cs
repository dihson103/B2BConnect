
using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Share;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventRepositoryTest;

public class SearchEventsAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventRepository _eventRepository;

    public SearchEventsAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventRepository = new EventRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var events = new List<Event>
        {
            Event.Create(
            new CreateEventCommand(
                "Name 1",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                "Image",
                null
                )
            ),
            Event.Create(
            new CreateEventCommand(
                "Name 2",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                "Image",
                null
                )
            ),
            Event.Create(
            new CreateEventCommand(
                "Name 3",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                "Image",
                null
                )
            ),
        };

        events[0].UpdateStatus(EventStatus.CANCELLED);

        _context.Events.AddRange(events);
        _context.SaveChanges();
    }

    [Fact]
    public async Task SearchEventsAsync_ShouldReturnFilteredEventsByStatus()
    {
        // Arrange
        var request = new GetEventsQuery("", EventStatus.CANCELLED, 1, 10);

        // Act
        var (events, totalPages) = await _eventRepository.SearchEventsAsync(request);

        // Assert
        Assert.Equal(1, events.Count);
        Assert.Equal(1, totalPages);
    }

    [Fact]
    public async Task SearchEventsAsync_ShouldReturnFilteredEventsBySearchTerm()
    {
        // Arrange
        var request = new GetEventsQuery("3", EventStatus.PLANNING, 1, 10);

        // Act
        var (events, totalPages) = await _eventRepository.SearchEventsAsync(request);

        // Assert
        Assert.Single(events);
        Assert.Equal("Name 3", events.First().Name);
    }

    [Fact]
    public async Task SearchEventsAsync_ShouldReturnEmptyAndTotalPages0_WhenNotFound()
    {
        // Arrange
        var request = new GetEventsQuery("son", EventStatus.PLANNING, 1, 10);

        // Act
        var (events, totalPages) = await _eventRepository.SearchEventsAsync(request);

        // Assert
        Assert.Equal(events.Count, 0);
        Assert.Equal(0, totalPages);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
