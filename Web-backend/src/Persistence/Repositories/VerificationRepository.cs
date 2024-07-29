using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class VerificationRepository : IVerificationRepository
{
    private readonly AppDbContext _context;
    public VerificationRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add(Verification verification)
    {
        _context.Verifications.Add(verification);
    }

    public async Task<Verification> GetById(Guid verId)
    {
        return await _context.Verifications
             .AsNoTracking()
                .SingleOrDefaultAsync(v => v.Id == verId);
    }

    public void Update(Verification verification)
    {
        _context.Verifications.Update(verification);
    }
}
