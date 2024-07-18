using Application.Abstractions.Data;
using Contract.Services.Branch.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.BranchRepositoryTest;
public class CreateBranchTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IBranchRepository _branchRepository;

    public CreateBranchTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _branchRepository = new BranchRepository(_context);
    }

    [Fact]
    public async Task Add_ShouldAddBranchToDatabase()
    {
        // Arrange
        var newBranch = Branch.Create(
            new CreateBranchCommand(
                "email@example.com",
                "1234567890",
                "123 Main St",
                true,
                Guid.NewGuid()
            )
        );

        // Act
        _branchRepository.Add(newBranch);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedBranch = await _context.Branches.FindAsync(newBranch.Id);
        Assert.NotNull(retrievedBranch);
        Assert.Equal(newBranch.Email, retrievedBranch.Email);
        Assert.Equal(newBranch.Phone, retrievedBranch.Phone);
        Assert.Equal(newBranch.Address, retrievedBranch.Address);
        Assert.Equal(newBranch.IsMainBranch, retrievedBranch.IsMainBranch);
        Assert.Equal(newBranch.BusinessId, retrievedBranch.BusinessId);
    }

    [Fact]
    public async Task Add_ShouldNotThrow_WhenAddingNullBranch()
    {
        // Arrange
        Branch nullBranch = null;

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(async () =>
        {
            _branchRepository.Add(nullBranch!);
            await _context.SaveChangesAsync();
        });
    }

    


    public void Dispose()
    {
        _context.Dispose();
    }

}
