﻿using Application.Abstractions.Data;
using Application.Abstractions.Services;
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
    private readonly Mock<IMediaRepository> _mediaRepositoryMock;
    private readonly Mock<IEventMediaRepository> _eventMediaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IRequestContext> _requestContextMock;
    private readonly CreateEventCommandHandler _handler;

    public CreateEventCommandHandlerTest()
    {
        _eventRepositoryMock = new Mock<IEventRepository>();
        _eventIndustryRepositoryMock = new Mock<IEventIndustryRepository>();
        _industryRepositoryMock = new Mock<IIndustryRepository>();
        _requestContextMock = new Mock<IRequestContext>();
        _eventMediaRepositoryMock = new Mock<IEventMediaRepository>();
        _mediaRepositoryMock = new Mock<IMediaRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _validator = new CreateEventValidator(_industryRepositoryMock.Object);

        _handler = new CreateEventCommandHandler(
            _eventRepositoryMock.Object,
            _eventIndustryRepositoryMock.Object,
            _mediaRepositoryMock.Object,
            _eventMediaRepositoryMock.Object,
            _requestContextMock.Object,
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
            industryIds,
            null);

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
            new List<Guid> { Guid.NewGuid() },
            null);

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
            new List<Guid> { Guid.NewGuid() },
            null);

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
            new List<Guid> { Guid.NewGuid() },
            null);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
        });
    }

    [Theory]
    [InlineData("", "location")]
    [InlineData("name", "")]
    [InlineData("", "")]
    public async Task Handler_ShouldThrowMyException_WhenValidationFails(string name, string location)
    {
        var createEventCommand = new CreateEventCommand(
            name,
            "description",
            DateTime.UtcNow.AddHours(8),
            DateTime.UtcNow.AddHours(9),
            location,
            new List<Guid> { Guid.NewGuid() },
            null);

        _industryRepositoryMock
            .Setup(repo => repo.IsAllIndustryIdsExistAsync(It.IsAny<List<Guid>>()))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyValidationException>(async () =>
        {
            var result = await _handler.Handle(createEventCommand, default);
        });
    }
}
