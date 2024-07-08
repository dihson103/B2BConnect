using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.IndustryRepositoryTest;
public class SearchIndustrieAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IIndustryRepository _industryRepository;

    public SearchIndustrieAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _industryRepository = new IndustryRepository(_context);

        InitDb();
    }

    private void InitDb()
    {
        var industries = new List<Industry>
        {
            Industry.Create("industry 2"),
            Industry.Create("industry 3"),
            Industry.Create("industry 4"),
            Industry.Create("industry 1"),
        };
        _context.Industries.AddRange(industries);
        _context.SaveChanges();
    }

    [Fact]
    public async Task ReturnAll_WhenSearchTermEmpty()
    {
        var searchTerm = "";

        var result = await _industryRepository.SearchIndustrieAsync(searchTerm);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task ReturnAll_WhenSearchTermNull()
    {
        string searchTerm = null;

        var result = await _industryRepository.SearchIndustrieAsync(searchTerm);

        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public async Task ReturnOne_WhenHaveOneItemValid()
    {
        string searchTerm = "1";

        var result = await _industryRepository.SearchIndustrieAsync(searchTerm);

        Assert.NotNull(result);
        Assert.Equal(1, result.Count);
    }

    [Fact]
    public async Task ReturnEmpty_WhenNotFound()
    {
        string searchTerm = "55";

        var result = await _industryRepository.SearchIndustrieAsync(searchTerm);

        Assert.NotNull(result);
        Assert.Equal(0, result.Count);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
