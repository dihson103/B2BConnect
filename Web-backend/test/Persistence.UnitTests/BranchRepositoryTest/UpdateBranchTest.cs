using Application.Abstractions.Data;
using Contract.Services.Branch.Create;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.BranchRepositoryTest;

public class UpdateBranchTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IBranchRepository _branchRepository;

    public UpdateBranchTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _branchRepository = new BranchRepository(_context);
    }

    [Fact]
    public async Task Update_ShouldModifyExistingBranch()
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

        _branchRepository.Add(newBranch);
        await _context.SaveChangesAsync();

        // Act
        newBranch.Email = "updated@example.com";
        newBranch.Phone = "0987654321";
        newBranch.Address = "456 Updated St";
        _branchRepository.Update(newBranch);
        await _context.SaveChangesAsync();

        // Assert
        var updatedBranch = await _context.Branches.FindAsync(newBranch.Id);
        Assert.NotNull(updatedBranch);
        Assert.Equal("updated@example.com", updatedBranch.Email);
        Assert.Equal("0987654321", updatedBranch.Phone);
        Assert.Equal("456 Updated St", updatedBranch.Address);
    }

    [Fact]
    public async Task Update_ShouldNotChangeBranchCount()
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

        _branchRepository.Add(newBranch);
        await _context.SaveChangesAsync();

        var initialCount = await _context.Branches.CountAsync();

        // Act
        newBranch.Email = "updated@example.com";
        _branchRepository.Update(newBranch);
        await _context.SaveChangesAsync();

        // Assert
        var finalCount = await _context.Branches.CountAsync();
        Assert.Equal(initialCount, finalCount);
    }

    [Fact]
    public async Task Update_ShouldThrow_WhenBranchIsNull()
    {
        // Arrange
        Branch nullBranch = null;

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
        {
            _branchRepository.Update(nullBranch);
            return _context.SaveChangesAsync();
        });
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
