using Application.Abstractions.Services;
using Contract.Services.Business.Share;
using Domain.Entities;
using Persistence;

namespace WebApi.InitialData;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            SeedData(scope.ServiceProvider);
        }
    }

    public static void SeedData(IServiceProvider provider)
    {
        var context = provider.GetService<AppDbContext>();

        if(!context.Industries.Any())
        {
            SeedingIndustryData(context);
        }

        if(!context.Accounts.Any())
        {
            var passwordService = provider.GetService<IPasswordService>();
            SeedingAccountData(context, passwordService);
        }

        if(!context.Businesses.Any())
        {
            SeedingBusinessData(context);
        }

        if (!context.Sectors.Any())
        {
            SeedingSectorData(context);
        }
    }

    private static void SeedingSectorData(AppDbContext context)
    {
        var businesses = context.Businesses.ToList();
        var industries = context.Industries.ToList();
        var sectors = new List<Sector>()
        {
            Sector.Create(businesses[0].Id, industries[0].Id),
            Sector.Create(businesses[0].Id, industries[1].Id),
            Sector.Create(businesses[0].Id, industries[2].Id),
            Sector.Create(businesses[1].Id, industries[3].Id),
        };

        context.Sectors.AddRange(sectors);
        context.SaveChanges();
    }

    private static void SeedingBusinessData(AppDbContext context)
    {
        var accounts = context.Accounts.ToList();
        var businesses = new List<Business>
        {
            new Business()
            {
                TaxCode = "000000000001",
                AccountId = accounts[1].Id,
                DateOfEstablishment = new DateOnly(2019, 5, 21),
                NumberOfEmployee = NumberOfEmployee.FROM_50_TO_100,
                IsVerified = true,
                Name = "Công ty ABC",
                CreatedBy = accounts[1].Id.ToString(),
                UpdatedDate = DateTime.UtcNow
            },
            new Business()
            {
                TaxCode = "000000000002",
                AccountId = accounts[2].Id,
                DateOfEstablishment = new DateOnly(2019, 5, 21),
                NumberOfEmployee = NumberOfEmployee.FROM_50_TO_100,
                IsVerified = true,
                Name = "Công ty EGH",
                CreatedBy = accounts[2].Id.ToString(),
                UpdatedDate = DateTime.UtcNow
            }
        };

        context.Businesses.AddRange(businesses);
        context.SaveChanges();
    }

    private static void SeedingAccountData(AppDbContext context, IPasswordService passwordService)
    {
        var accounts = new List<Account>()
        {
            Account.Create("admin@gmail.com", passwordService.Hash("12345")),
            Account.Create("businessA@gmail.com", passwordService.Hash("12345")),
            Account.Create("businessB@gmail.com", passwordService.Hash("12345"))
        };
        accounts[0].IsAdmin = true;

        context.Accounts.AddRange(accounts);
        context.SaveChanges();
    }

    private static void SeedingIndustryData(AppDbContext context)
    {
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

        context.Industries.AddRange(industries);
        context.SaveChanges();
    }
}
