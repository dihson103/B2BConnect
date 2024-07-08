using Application.Abstractions.Data;
using Application.UseCases.Queries.Industries.GetIndustriesByEventId;
using Contract.Services.Industry.GetIndustriesOfEvent;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Queries.Industries;
public class GetIndustriesByEventIdQueryHandlerTest
{
    private readonly GetIndustriesByEventIdQueryHandler handler;
    private readonly Mock<IEventIndustryRepository> _eventIndustryRepositoryMock;
    public GetIndustriesByEventIdQueryHandlerTest()
    {
        _eventIndustryRepositoryMock = new();
        handler = new GetIndustriesByEventIdQueryHandler(_eventIndustryRepositoryMock.Object);
    }

    [Fact]
    public async Task Hanlder_ShouldReturnNullData_WhenNotFound()
    {
        var getIndustriesByEventIdQuery = new GetIndustriesByEventIdQuery(Guid.NewGuid());

        _eventIndustryRepositoryMock
            .Setup(repo => repo.GetByEventIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<EventIndustry> { });

        var result = await handler.Handle(getIndustriesByEventIdQuery, default);

        Assert.NotNull(result);
        Assert.Equal(0, result.Data.Count);
    }

    [Fact]
    public async Task Hanlder_ShouldReturnData_WhenFound()
    {
        var getIndustriesByEventIdQuery = new GetIndustriesByEventIdQuery(Guid.NewGuid());
        var industry = Industry.Create("aaa");
        var eventIndustry = EventIndustry.Create(Guid.NewGuid(), industry.Id);
        eventIndustry.Industry = industry;


        _eventIndustryRepositoryMock
            .Setup(repo => repo.GetByEventIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<EventIndustry> { eventIndustry});

        var result = await handler.Handle(getIndustriesByEventIdQuery, default);

        Assert.NotNull(result);
        Assert.Equal(1, result.Data.Count);
    }
}
