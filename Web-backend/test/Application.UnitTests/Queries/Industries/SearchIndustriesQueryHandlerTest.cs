using Application.Abstractions.Data;
using Application.UseCases.Queries.Industries.SearchIndustries;
using Contract.Services.Industry.SearchIndustries;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Queries.Industries;
public class SearchIndustriesQueryHandlerTest
{
    private readonly SearchIndustriesQueryHandler handler;
    private readonly Mock<IIndustryRepository> _industryRepositoryMock;

    public SearchIndustriesQueryHandlerTest()
    {
        _industryRepositoryMock = new();
        handler = new SearchIndustriesQueryHandler( _industryRepositoryMock.Object );
    }

    [Fact]
    public async Task Hanlder_ShouldReturnNullData_WhenNotFound()
    {
        var searchIndustriesQuery = new SearchIndustriesQuery("aaaa");

        _industryRepositoryMock
            .Setup(repo => repo.SearchIndustrieAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Domain.Entities.Industry> { });

        var result = await handler.Handle(searchIndustriesQuery, default);

        Assert.NotNull(result);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Hanlder_ShouldReturnData_WhenFound()
    {
        var searchIndustriesQuery = new SearchIndustriesQuery("aaaa");

        _industryRepositoryMock
            .Setup(repo => repo.SearchIndustrieAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<Industry> { Industry.Create("aaaaaa") });

        var result = await handler.Handle(searchIndustriesQuery, default);

        Assert.NotNull(result);
        Assert.Equal(1, result.Data.Count);
    }
}
