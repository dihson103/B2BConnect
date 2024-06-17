using Microsoft.EntityFrameworkCore;

namespace Persistence;
internal class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}
