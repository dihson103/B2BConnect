using Application.Abstractions.Data;
using Application.UseCases.Queries.Events.GetEvents;
using AutoMapper;
using Contract.Services.Event.GetEvents;
using Contract.Services.Event.Share;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Queries.Events;
public class GetEventsQueryHandlerTest
{
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetEventsQueryHandler handler;

    public GetEventsQueryHandlerTest()
    {
        _eventRepositoryMock = new();
        _mapperMock = new();
        handler = new GetEventsQueryHandler(_eventRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Hanlder_ShouldReturnNullData_WhenNotFound()
    {
        var getEventsQuery = new GetEventsQuery("dsfsd", EventStatus.PLANNING);

        _eventRepositoryMock
            .Setup(repo => repo.SearchEventsAsync(getEventsQuery))
            .ReturnsAsync((new List<Event> {}, 0, 0));

        var result = await handler.Handle(getEventsQuery, default);

        Assert.NotNull(result);
        Assert.Null(result.Data);
        Assert.True(result.Status == 200);
    }

    [Fact]
    public async Task Hanlder_ShouldReturnData_WhenFound()
    {
        var getEventsQuery = new GetEventsQuery("dsfsd", EventStatus.PLANNING);

        _eventRepositoryMock
            .Setup(repo => repo.SearchEventsAsync(getEventsQuery))
            .ReturnsAsync((new List<Event> { Event.Create() }, 1, 1));

        var result = await handler.Handle(getEventsQuery, default);

        Assert.NotNull(result);
        Assert.Equal(1, result.Data.Data.Count);
        Assert.True(result.Status == 200);
    }
}
