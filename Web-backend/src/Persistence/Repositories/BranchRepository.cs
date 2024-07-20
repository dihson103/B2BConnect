using Application.Abstractions.Data;
using Contract.Services.Branch.GetBranches;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _context;
    public BranchRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Branch branch)
    {
        _context.Branches.Add(branch);
    }

    public void AddRange(List<Branch> branches)
    {
       _context.Branches.AddRange(branches);
    }

    public void Delete(Branch branch)
    {
        _context.Branches.Remove(branch);
    }

    public void DeleteByBusinessId(Guid id)
    {
        var representatives = _context.Branches.Where(r => r.BusinessId == id);

        if (representatives.Any())
        {
            _context.Branches.RemoveRange(representatives);
        }
    }

    public async Task<List<Branch>> GetBranchesAsync(GetBranchesQuery getBranchesQuery)
    {
        var query = _context.Branches!.Where(b => b!.BusinessId == getBranchesQuery.BusinessId);
        var branches = await query
           .OrderBy(b => b.Id)
            .AsNoTracking()
            .AsSingleQuery()
           .ToListAsync();
        return branches;
    }

    public async Task<Branch> GetByIdAsync(Guid id)
    {
        return await _context.Branches
                     .AsNoTracking()
                    .AsSplitQuery()
                    .SingleOrDefaultAsync(e => e.Id == id);
    }

    public void Update(Branch branch)
    {
        _context.Branches.Update(branch);
    }
}
