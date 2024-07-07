using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.IndustryRepositoryTest;
public class AddRangeTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IIndustryRepository _industryRepository;

    public AddRangeTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _industryRepository = new IndustryRepository(_context);
    }

    [Fact]
    public async Task Should_AddNewIndudtriesToDb()
    {
        // Arrange
        var industries = new List<Industry>
        {
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
            Industry.Create("industry 1"),
        };

        // Act
        _industryRepository.AddRange(industries);
        await _context.SaveChangesAsync();

        // Assert
        var count = _context.Industries.Count();
        Assert.Equal(4, count);
    }

    [Fact]
    public async Task Should_ThrowException_WhenListNull()
    {

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            _industryRepository.AddRange(null);
            await _context.SaveChangesAsync();
        });
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
