using Application.Abstractions.Data;
using Domain.Entities;

namespace Persistence.Repositories;
public class RepresentativeRepository : IRepresentativeRepository
{
    private readonly AppDbContext _context;
    public RepresentativeRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add(Representative representative)
    {
        _context.Representatives.Add(representative);
    }

    public void DeleteByBusinessId(Guid businessId)
    {
        var representatives = _context.Representatives.Where(r => r.BusinessId == businessId);

        if (representatives.Any())
        {
            _context.Representatives.RemoveRange(representatives);
        }
    }

    public void Update(Representative re)
    {
        _context.Representatives.Update(re);
    }
}
