using Application.Abstractions.Data;
using Application.UseCases.Commands.Events.RequestJoin;
using Contract.Services.Event.RequestJoin;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Commands.Events;
public class RequestJoinCommandHanlderTest
{
    private readonly RequestJoinCommandHanlder handler;
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IBusinessRepository> _businessRepositoryMock;
    private readonly Mock<ISectorRepository> _sectorRepositoryMock;
    private readonly Mock<IEventIndustryRepository> _eventIndustryRepositoryMock;
    private readonly Mock<IParticipationRepository> _participationRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    public RequestJoinCommandHanlderTest()
    {
        _eventRepositoryMock = new();
        _businessRepositoryMock = new();
        _sectorRepositoryMock = new();
        _eventIndustryRepositoryMock = new();
        _participationRepositoryMock = new();
        _unitOfWorkMock = new();
        handler = new RequestJoinCommandHanlder(
            _eventRepositoryMock.Object,
            _businessRepositoryMock.Object,
            _sectorRepositoryMock.Object,
            _eventIndustryRepositoryMock.Object,
            _participationRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldThrowMyNotFoundException_WhenBusinessIdNotFound()
    {
        var requestJoinCommand = new RequestJoinCommand(Guid.NewGuid(), Guid.NewGuid());

        _businessRepositoryMock
            .Setup(repo => repo.IsBusinessValidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<MyNotFoundException>( async () =>
        {
            var result = await handler.Handle(requestJoinCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowMyNotFoundException_WhenEventIdNotFound()
    {
        var requestJoinCommand = new RequestJoinCommand(Guid.NewGuid(), Guid.NewGuid());

        _businessRepositoryMock
            .Setup(repo => repo.IsBusinessValidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.IsEventValidToJoinAsync(It.IsAny<Guid>()))  
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<MyNotFoundException>(async () =>
        {
            var result = await handler.Handle(requestJoinCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowMyBadRequestException_WhenAlreadyJoin()
    {
        var requestJoinCommand = new RequestJoinCommand(Guid.NewGuid(), Guid.NewGuid());

        _businessRepositoryMock
            .Setup(repo => repo.IsBusinessValidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.IsEventValidToJoinAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _participationRepositoryMock
            .Setup(repo => repo.IsBusinessRequestedAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))   
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<MyBadRequestException>(async () =>
        {
            var result = await handler.Handle(requestJoinCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldThrowMyNotFoundException_WhenSectorsOfBusinessNotFound()
    {
        var requestJoinCommand = new RequestJoinCommand(Guid.NewGuid(), Guid.NewGuid());

        _businessRepositoryMock
            .Setup(repo => repo.IsBusinessValidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.IsEventValidToJoinAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _participationRepositoryMock
            .Setup(repo => repo.IsBusinessRequestedAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        _sectorRepositoryMock
            .Setup(repo => repo.GetSectorsByBusinessIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Sector> { });

        await Assert.ThrowsAsync<MyNotFoundException>(async () =>
        {
            var result = await handler.Handle(requestJoinCommand, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldReturnSuccessResult()
    {
        var requestJoinCommand = new RequestJoinCommand(Guid.NewGuid(), Guid.NewGuid());

        _businessRepositoryMock
            .Setup(repo => repo.IsBusinessValidAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _eventRepositoryMock
            .Setup(repo => repo.IsEventValidToJoinAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
        _participationRepositoryMock
            .Setup(repo => repo.IsBusinessRequestedAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        _sectorRepositoryMock
            .Setup(repo => repo.GetSectorsByBusinessIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Sector> { Sector.Create(Guid.NewGuid(), Guid.NewGuid()) });
        _eventIndustryRepositoryMock
            .Setup(repo => repo.IsInEventIndustriesAsync(It.IsAny<List<Guid>>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);

        var result = await handler.Handle(requestJoinCommand, default);

        Assert.NotNull(result);
        Assert.True(result.Status == 200);
    }
}
