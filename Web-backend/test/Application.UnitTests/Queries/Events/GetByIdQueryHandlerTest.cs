using Application.Abstractions.Data;
using Application.UseCases.Queries.Events.GetEvent;
using AutoMapper;
using Contract.Services.Event.GetById;
using Contract.Services.Event.Share;
using Contract.Services.Industry.Share;
using Domain.Abstractioins.Exceptions;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Queries.Events;
public class GetByIdQueryHandlerTest
{
    private readonly GetByIdQueryHandler handler;
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    public GetByIdQueryHandlerTest()
    {
        _eventRepositoryMock = new();
        _mapperMock = new Mock<IMapper>();
        handler = new GetByIdQueryHandler(_eventRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldThrowMyNotFoundException_WhenNotFound()
    {
        var getByIdQuery = new GetByIdQuery(Guid.NewGuid());

        _eventRepositoryMock
            .Setup(repo => repo.GetByIdIncludeIndustriesAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Event)null);

        await Assert.ThrowsAsync<MyNotFoundException>(async () =>
        {
            var result = await handler.Handle(getByIdQuery, default);
        });
    }

    [Fact]
    public async Task Handler_ShouldReturnResult_WhenFound()
    {
        var fakeIndustry1 = new IndustryResponse(Guid.NewGuid(), "Technology");
        var fakeIndustry2 = new IndustryResponse(Guid.NewGuid(), "Healthcare");

        var fakeEvent = new SingleEventResponse(
            Id: Guid.NewGuid(),
            Name: "Tech Conference 2024",
            Description: "A conference for technology enthusiasts and professionals.",
            StartAt: new DateTime(2024, 8, 1, 9, 0, 0),
            EndAt: new DateTime(2024, 8, 1, 17, 0, 0),
            Location: "San Francisco, CA",
            null,
            Status: EventStatus.CANCELLED,
            StatusDescription: "The event is currently ongoing.",
            Industries: new List<IndustryResponse> { fakeIndustry1, fakeIndustry2 }
        );
        var getByIdQuery = new GetByIdQuery(Guid.NewGuid());

        _eventRepositoryMock
            .Setup(repo => repo.GetByIdIncludeIndustriesAsync(It.IsAny<Guid>()))
            .ReturnsAsync(Event.Create());
        _mapperMock
            .Setup(mapper => mapper.Map<SingleEventResponse>(It.IsAny<Event>()))
            .Returns(fakeEvent);

         var result = await handler.Handle(getByIdQuery, default);

        Assert.NotNull(result);
        Assert.True(result.Status == 200);
    }
}
