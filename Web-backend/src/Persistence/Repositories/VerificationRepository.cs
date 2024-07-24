using Application.Abstractions.Data;
using Domain.Entities;

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

    public void Update(Verification verification)
    {
        _context.Verifications.Update(verification);
    }
}
