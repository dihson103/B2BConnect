using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.IndustryRepositoryTest;
public class IsAllIndustryIdsExistAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IIndustryRepository _industryRepository;

    public IsAllIndustryIdsExistAsyncTest()
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
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
        };
        _context.Industries.AddRange(industries);
        _context.SaveChanges();
    }

    [Fact] 
    public async Task ReturnTrue_WhenIdsExist()
    {
        //Arrange
        var industry = _context.Industries.FirstOrDefault();
        var idustryIds = new List<Guid> { industry.Id };

        // Act
        var result = await _industryRepository.IsAllIndustryIdsExistAsync(idustryIds);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ReturnFalse_WhenIdsNotExist()
    {
        //Arrange
        var idustryIds = new List<Guid> { Guid.NewGuid() };

        // Act
        var result = await _industryRepository.IsAllIndustryIdsExistAsync(idustryIds);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ReturnFalse_WhenThereAtLeastOneIdNotExist()
    {
        //Arrange
        var industry = _context.Industries.FirstOrDefault();
        var idustryIds = new List<Guid> { Guid.NewGuid(), industry.Id };

        // Act
        var result = await _industryRepository.IsAllIndustryIdsExistAsync(idustryIds);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ReturnFalse_WhenIdsEmpty()
    {
        //Arrange
        var idustryIds = new List<Guid> {};

        // Act
        var result = await _industryRepository.IsAllIndustryIdsExistAsync(idustryIds);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task ReturnFalse_WhenIdsNull()
    {

        // Act
        var result = await _industryRepository.IsAllIndustryIdsExistAsync(null);

        // Assert
        Assert.False(result);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
