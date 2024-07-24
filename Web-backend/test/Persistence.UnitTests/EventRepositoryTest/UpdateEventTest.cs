using Application.Abstractions.Data;
using Contract.Services.Event.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.EventRepositoryTest;

public class UpdateEventTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IEventRepository _eventRepository;

    public UpdateEventTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _eventRepository = new EventRepository(_context);
    }

    [Fact]
    public async Task Update_ShouldModifyExistingEvent()
    {
        // Arrange
        var newEvent = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                null, null
            ),
            "097733"
        );

        _eventRepository.Add(newEvent);
        await _context.SaveChangesAsync();

        // Act
        newEvent.Name = "Updated Name";
        newEvent.Description = "Updated Description";
        newEvent.Location = "Updated Location";
        _eventRepository.Update(newEvent);
        await _context.SaveChangesAsync();

        // Assert
        var updatedEvent = await _context.Events.FindAsync(newEvent.Id);
        Assert.NotNull(updatedEvent);
        Assert.Equal("Updated Name", updatedEvent.Name);
        Assert.Equal("Updated Description", updatedEvent.Description);
        Assert.Equal("Updated Location", updatedEvent.Location);
    }

    [Fact]
    public async Task Update_ShouldNotChangeEventCount()
    {
        // Arrange
        var newEvent = Event.Create(
            new CreateEventCommand(
                "Name",
                "Description",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddHours(2),
                "Location",
                null, null
            ),
            "0987333"
        );

        _eventRepository.Add(newEvent);
        await _context.SaveChangesAsync();

        var initialCount = await _context.Events.CountAsync();

        // Act
        newEvent.Name = "Updated Name";
        _eventRepository.Update(newEvent);
        await _context.SaveChangesAsync();

        // Assert
        var finalCount = await _context.Events.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

    [Fact]
    public async Task Update_ShouldThrow_WhenEventIsNull()
    {
        // Arrange
        Event nullEvent = null;

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
        {
            _eventRepository.Update(nullEvent);
            return _context.SaveChangesAsync();
        });
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
