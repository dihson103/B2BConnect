using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventRepositoryTest;

public class AddEventTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventRepository _eventRepository;

    public AddEventTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventRepository = new EventRepository(_context);
    }

    [Fact]
    public async Task Add_ShouldAddEventToDatabase()
    {
        // Arrange
        var newEvent = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                "Image",
                null
                )
            );

        // Act
        _eventRepository.Add(newEvent);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedEvent = await _context.Events.FindAsync(newEvent.Id);
        Assert.NotNull(retrievedEvent);
        Assert.Equal(newEvent.Name, retrievedEvent.Name);
        Assert.Equal(newEvent.Description, retrievedEvent.Description);
        Assert.Equal(newEvent.StartAt, retrievedEvent.StartAt);
        Assert.Equal(newEvent.EndAt, retrievedEvent.EndAt);
        Assert.Equal(newEvent.Status, retrievedEvent.Status);
        Assert.Equal(newEvent.Location, retrievedEvent.Location);
    }

    [Fact]
    public async Task Add_ShouldNotThrow_WhenAddingNullEvent()
    {
        // Arrange
        Event nullEvent = null;

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            _eventRepository.Add(nullEvent);
            await _context.SaveChangesAsync();
        });
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
