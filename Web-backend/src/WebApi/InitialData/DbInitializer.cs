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
