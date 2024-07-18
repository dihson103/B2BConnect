using Application.Abstractions.Data;
using Application.UseCases.Commands.Events.Create;
using Contract.Services.Event.Create;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using FluentValidation;
using Moq;
using Xunit;

namespace Application.UnitTests.Commands.Events;

public class CreateEventCommandHandlerTest
{
    private readonly IValidator<CreateEventCommand> _validator;
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IEventIndustryRepository> _eventIndustryRepositoryMock;
    private readonly Mock<IIndustryRepository> _industryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateEventCommandHandler _handler;

    public CreateEventCommandHandlerTest()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _eventIndustryRepositoryMock = new Mock<IEventIndustryRepository>();
        _industryRepositoryMock = new Mock<IIndustryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new CreateEventValidator(_industryRepositoryMock.Object);

        _handler = new CreateEventCommandHandler(
            _eventRepositoryMock.Object,
            _eventIndustryRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validator);
    }

    [Fact]
    public async Task Handler_ShouldAddEventAndEventIndustries_WhenRequestIsValid()
    {
        var industryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var createEventCommand = new CreateEventCommand(
            "name",
            "description",
            DateTime.UtcNow.AddHours(9),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            industryIds);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);


        var result = await _handler.Handle(createEventCommand, default);

        _eventRepositoryMock.Verify(repo => repo.Add(It.IsAny<Event>()), Times.Once);
        _eventIndustryRepositoryMock.Verify(repo => repo.AddRange(It.IsAny<List<EventIndustry>>()), Times.Once);

        Assert.True(result.Status == 200);
        Assert.Equal("Tạo sự kiện thành công", result.Message);
    }

    [Fact]
    public async Task Handler_ShouldThrowMyException_WhenIndustryIdNotExist()
    {
        var createEventCommand = new CreateEventCommand(
            "name",
            "description",
            DateTime.UtcNow.AddHours(9),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            new List<Guid> { Guid.NewGuid() });

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowMyException_WhenStartDateLessThanCurrentDate()
    {
        var createEventCommand = new CreateEventCommand(
            "name",
            "description",
            DateTime.UtcNow.AddHours(4),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            new List<Guid> { Guid.NewGuid() });

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowMyException_WhenEndDateLessThanStartDate()
    {
        var createEventCommand = new CreateEventCommand(
            "name",
            "description",
            DateTime.UtcNow.AddHours(11),
            DateTime.UtcNow.AddHours(10),
            "Location",
            "Image",
            new List<Guid> { Guid.NewGuid() });

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
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
        var createEventCommand = new CreateEventCommand(
            name,
            "description",
            DateTime.UtcNow.AddHours(8),
            DateTime.UtcNow.AddHours(9),
            location,
            image,
            new List<Guid> { Guid.NewGuid() });

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
        });
    }
}
