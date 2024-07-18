using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.BusinessRepositoryTest;
public class GetBusinessesAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly IBusinessRepository _businessRepository;

    public GetBusinessesAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
          .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _businessRepository = new BusinessRepository(_context);

        SeedDatabase();
    }

    private void SeedDatabase()
    {
       
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
