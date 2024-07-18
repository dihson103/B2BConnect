using Application.Abstractions.Data;
using Application.UseCases.Commands.Events.Update;
using Contract.Services.Event.Update;
using FluentValidation;
using Moq;
using Contract.Services.Event.Share;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;

namespace Application.UnitTests.Commands.Events;
public class UpdateEventCommandHandlerTest
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IIndustryRepository> _industryRepositoryMock;
    private readonly IValidator<UpdateEventRequest> _validator;
    private readonly UpdateEventCommandHandler handler;

    public UpdateEventCommandHandlerTest()
    {
        _eventRepositoryMock = new();
        _unitOfWorkMock = new();
        _industryRepositoryMock = new();
        _validator = new UpdateEventValidator(_industryRepositoryMock.Object);
        handler = new(_eventRepositoryMock.Object, _unitOfWorkMock.Object, _validator);
    }

    [Fact]
    public async Task Hanlder_ShouldThrowMyNotFoundException_WhenEventNotFound()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            "name",
            "description",
            DateTime.UtcNow.AddHours(9),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Event)null);

        await Assert.ThrowsAsync<MyNotFoundException>(async () =>
        {
            await handler.Handle(updateEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShoulThrowMyValidationException_WhenStartDateLessThanCurrentDate()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            "name",
            "description",
            DateTime.UtcNow.AddHours(3),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            await handler.Handle(updateEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShoulThrowMyValidationException_WhenEndDateLessThanStartDate()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            "name",
            "description",
            DateTime.UtcNow.AddHours(10),
            DateTime.UtcNow.AddHours(9),
            "Location",
            "Image",
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            await handler.Handle(updateEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShoulThrowMyValidationException_WhenIndustryIdsNotExist()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            "name",
            "description",
            DateTime.UtcNow.AddHours(8),
            DateTime.UtcNow.AddHours(9),
            "Location",
            "Image",
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(false);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            await handler.Handle(updateEventCommand, default);
        });
    }

    [Theory]
    [InlineData("", "location", "image")]
    [InlineData("name", "", "image")]
    [InlineData("name", "location", "")]
    [InlineData("name", "", "")]
    [InlineData("", "", "image")]
    [InlineData("", "location", "")]
    [InlineData("", "", "")]
    public async Task Handler_ShouldThrowMyException_WhenValidationFails(string name, string location, string image)
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            name,
            "description",
            DateTime.UtcNow.AddHours(8),
            DateTime.UtcNow.AddHours(9),
            location,
            image,
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            await handler.Handle(updateEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccessResult()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var updateEventRequest = new UpdateEventRequest(
            "name",
            "description",
            DateTime.UtcNow.AddHours(8),
            DateTime.UtcNow.AddHours(9),
            "Location",
            "Image",
            industryIds,
            EventStatus.PLANNING);
        var updateEventCommand = new UpdateEventCommand(Guid.NewGuid(), updateEventRequest);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());

        var result = await handler.Handle(updateEventCommand, default);

        Assert.NotNull(result);
        Assert.True(result.Status == 200);
    }
}
