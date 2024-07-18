
using Application.Abstractions.Data;
using Contract.Services.Branch.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.BranchRepositoryTest;
public class GetByIdAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IBranchRepository _branchRepository;

    public GetByIdAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _branchRepository = new BranchRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
        var events = new List<Branch>
        {
            Branch.Create(
            new CreateBranchCommand(
                "",
                "",
               "HN",
               true,
                new Guid("1f8a6212-d591-4cf2-9e4b-11c37823d2e5")
                )
            ),
           Branch.Create(
            new CreateBranchCommand(
                "codluck1@codluck.com",
                "0123123123",
               "HCM",
               true,
                new Guid("1f8a6212-d591-4cf2-9e4b-11c37823d2e5")
                )
            ),
           Branch.Create(
            new CreateBranchCommand(
                "codluck2@codluck.com",
                "0111222333",
               "Dubai",
               true,
                new Guid("1f8a6212-d591-4cf2-9e4b-11c37823d2e5")
                )
            )
        };

        _context.Branches.AddRange(events);
        _context.SaveChanges();
    }

    [Fact]
    public async Task ShouldReturnBranch_WhenIdExist()
    {
        var id = _context.Branches.FirstOrDefault().Id;

        var result = await _branchRepository.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.NotNull(result.BusinessId);
    }

    [Fact]
    public async Task ShouldReturnNull_WhenIdNotExist()
    {
        var id = Guid.NewGuid();

        var result = await _branchRepository.GetByIdAsync(id);

        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
