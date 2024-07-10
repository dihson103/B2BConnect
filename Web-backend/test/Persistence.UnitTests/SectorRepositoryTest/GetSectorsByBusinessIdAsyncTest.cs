using Application.Abstractions.Data;
using Application.Abstractions.Services;
using Contract.Services.Business.Share;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace Persistence.UnitTests.SectorRepositoryTest;
public class GetSectorsByBusinessIdAsyncTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly ISectorRepository _sectorRepository;

    public GetSectorsByBusinessIdAsyncTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new AppDbContext(optionsBuilder.Options);
        _sectorRepository = new SectorRepository(_context);

        SeedingData();
    }

    private void SeedingData()
    {
        var accounts = new List<Account>
        {
            Account.Create("admin@gmail.com", "pass"),
            Account.Create("businessA@gmail.com", "12345"),
            Account.Create("businessB@gmail.com", "12345")
        };

        _context.Accounts.AddRange(accounts);
        _context.SaveChanges(); 

        var businesses = new List<Business>
        {
            new Business
            {
                TaxCode = "000000000001",
                AccountId = accounts[1].Id,
                DateOfEstablishment = new DateOnly(2019, 5, 21),
                NumberOfEmployee = NumberOfEmployee.FROM_50_TO_100,
                IsVerified = true,
                Name = "Công ty ABC"
            },
            new Business
            {
                TaxCode = "000000000002",
                AccountId = accounts[2].Id,
                DateOfEstablishment = new DateOnly(2019, 5, 21),
                NumberOfEmployee = NumberOfEmployee.FROM_50_TO_100,
                IsVerified = true,
                Name = "Công ty EGH"
            }
        };

        _context.Businesses.AddRange(businesses);
        _context.SaveChanges(); 

        var industries = new List<Industry>
        {
            Industry.Create("Lĩnh vực công nghệ thông tin"),
            Industry.Create("Lĩnh vực Y tế – Dược phẩm"),
            Industry.Create("Lĩnh vực giáo dục"),
            Industry.Create("Lĩnh vực bất động sản – xây dựng"),
            Industry.Create("Lĩnh vực năng lượng và môi trường"),
            Industry.Create("Lĩnh vực thực phẩm – nông nghiệp"),
            Industry.Create("Lĩnh vực dịch vụ và du lịch"),
            Industry.Create("Lĩnh vực vận tải và logistics"),
        };

        _context.Industries.AddRange(industries);
        _context.SaveChanges(); 

        var sectors = new List<Sector>
        {
            Sector.Create(businesses[0].Id, industries[0].Id),
            Sector.Create(businesses[0].Id, industries[1].Id),
            Sector.Create(businesses[0].Id, industries[2].Id),
            Sector.Create(businesses[1].Id, industries[3].Id),
        };

        _context.Sectors.AddRange(sectors);
        _context.SaveChanges(); 
    }


    [Fact]
    public async Task ReturnEmpty_WhenNotFound()
    {
        var businessId = Guid.NewGuid();

        var result = await _sectorRepository.GetSectorsByBusinessIdAsync(businessId);

        Assert.Equal(0, result.Count);
    }

    [Fact]
    public async Task ReturnResultCountMoreThan0_WhenFound()
    {
        var businessId = _context.Businesses.First().Id;

        var result = await _sectorRepository.GetSectorsByBusinessIdAsync(businessId);

        Assert.True(result.Count > 0);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
