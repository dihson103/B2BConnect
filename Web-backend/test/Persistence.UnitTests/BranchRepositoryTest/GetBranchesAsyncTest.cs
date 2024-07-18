using Application.Abstractions.Data;
using Contract.Services.Branch.Create;
using Contract.Services.Branch.GetBranches;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.BranchRepositoryTest;
public class GetBranchesAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IBranchRepository _branchRepository;

    public GetBranchesAsyncTest()
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
    public async Task GetBranchesAsync_ReturnBranchesByVerifyStatus()
    {
        // Arrange
        var request = new GetBranchesQuery(new Guid("1f8a6212-d591-4cf2-9e4b-11c37823d2e5"));

        // Act
        var branches = await _branchRepository.GetBranchesAsync(request);

        // Assert
        Assert.Equal(3, branches.Count); ////check size of branches và so sánh list size trả về mong đợi = 3
    }

    [Fact]
    public async Task GetBranchesAsync_ReturnEmptyAndTotalPages0_WhenNotFound()
    {
        // Arrange
        var request = new GetBranchesQuery(new Guid("1f8a6212-d591-4cf2-9e4b-11c37823d2e3"));

        // Act
        var branches = await _branchRepository.GetBranchesAsync(request);

        // Assert
        Assert.Equal(0, branches?.Count); 
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
