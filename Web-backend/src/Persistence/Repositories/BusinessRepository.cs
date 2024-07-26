using System.Web;
using Application.Abstractions.Data;
using Application.Utils;
using Contract.Services.Business.GetBusinesses;
using Contract.Services.Business.Share;
using Contract.Services.Verification.Share;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class BusinessRepository : IBusinessRepository
{
    private readonly AppDbContext _context;
    public BusinessRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Business business)
    {
        _context.Businesses.Add(business);
    }

    public async Task<Business> getByAccountIdAsync(Guid id)
    {
        return await _context.Businesses
                    .AsNoTracking()
                     .AsSplitQuery()
                    .SingleOrDefaultAsync(b => b.AccountId == id);
    }

    public async Task<Business> GetByIdAsync(Guid id)
    {
        return await _context.Businesses
                     .AsNoTracking()
                     .AsSplitQuery()
                     .Include(e => e.Representative)
                     .Include(e => e.Account)
                    .Include(e => e.Branches!)
                    .Include(e => e.Sectors)
                                .ThenInclude(s => s.Industry)
                    .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> IsBusinessValidAsync(Guid id)
    {
        return await _context.Businesses
            .Include(b => b.Account)
            .AnyAsync(b => b.Id == id && b.Account.IsActive == true);
    }

    public async Task<(List<Business>?, int, int)> SearchBusinessAsync(GetBusinessesByUserQuery getBusinessesQuery)
    {
        var query = _context.Businesses
            .Include(b => b.Account)
            .Include(b => b.Branches)
            .AsQueryable();

        if (getBusinessesQuery.IndustryIds != null && getBusinessesQuery.IndustryIds.Any())
        {
            query = query
                .Where(b => b.Sectors != null && b.Sectors.Any(s => getBusinessesQuery.IndustryIds.Contains(s.IndustryId)));
        }

        if (getBusinessesQuery.NumberOfEmployee.HasValue)
        {
            query = query.Where(b => b.NumberOfEmployee == getBusinessesQuery.NumberOfEmployee.Value);
        }

        if (getBusinessesQuery.IsVerified.HasValue)
        {
            query = query.Where(b => b.IsVerified == getBusinessesQuery.IsVerified);
        }

        if (!string.IsNullOrEmpty(getBusinessesQuery.SearchTerm))
        {
            var searchTerm = HttpUtility.UrlDecode(getBusinessesQuery.SearchTerm); // Decode URL
            var searchTermNoDiacritics = StringHandlerUtil.RemoveDiacritics(searchTerm.ToLower());

            query = query
                .Where(b =>
                    b.Branches != null && b.Branches.Any(br => StringHandlerUtil.RemoveDiacritics(br.Address.ToLower()).Contains(searchTermNoDiacritics)) ||
                    StringHandlerUtil.RemoveDiacritics(b.Name.ToLower()).Contains(searchTermNoDiacritics)
                );
        }

        var businesses = await query
            .OrderBy(b => b.DateOfEstablishment)
            .AsNoTracking()
            .ToListAsync();

        if (getBusinessesQuery.NOYEstablished.HasValue)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var filteredBusinesses = businesses
                .Where(b =>
                {
                    var daysEstablished = CalculateDaysBetweenDates(b.DateOfEstablishment, currentDate);
                    var yearsEstablished = daysEstablished / 365.0; 

                    return getBusinessesQuery.NOYEstablished switch
                    {
                        NumberOfYearEstablished.LessThanOneYear => yearsEstablished < 1,
                        NumberOfYearEstablished.TwoToFiveYears => yearsEstablished >= 1 && yearsEstablished < 5,
                        NumberOfYearEstablished.FiveToTenYears => yearsEstablished >= 5 && yearsEstablished < 10,
                        NumberOfYearEstablished.TenToTwentyYears => yearsEstablished >= 10 && yearsEstablished < 20,
                        NumberOfYearEstablished.OverTwentyYears => yearsEstablished >= 20,
                        _ => false
                    };
                })
                .ToList();

            businesses = filteredBusinesses;
        }

        var totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / getBusinessesQuery.PageSize);

        businesses = businesses
            .Skip((getBusinessesQuery.PageIndex - 1) * getBusinessesQuery.PageSize)
            .Take(getBusinessesQuery.PageSize)
            .ToList();

        return (businesses, totalPages, totalItems);
    }


    private int CalculateDaysBetweenDates(DateOnly startDate, DateOnly endDate)
    {
        return (endDate.ToDateTime(TimeOnly.MinValue) - startDate.ToDateTime(TimeOnly.MinValue)).Days;
    }

    public async Task<(List<Business>?, int, int)> SearchBusinessesByAdminAsync(GetBusinessesByAdminQuery request)
    {
        var query = _context.Businesses!.AsQueryable();

        if (!string.IsNullOrEmpty(request.IsVerified.ToString()))
        {
            query = query.Where(b => b.IsVerified == request.IsVerified);
        }


        var businesses = await query
         .OrderBy(b => b.DateOfEstablishment)
         .AsNoTracking()
         .ToListAsync();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var s = HttpUtility.UrlDecode(request.SearchTerm); // Giải mã từ URL
            var searchTermNoDiacritics = StringHandlerUtil.RemoveDiacritics(s.ToLower());

            businesses = businesses
                .Where(b => StringHandlerUtil.RemoveDiacritics(b.Name.ToLower()).Contains(searchTermNoDiacritics))
                .ToList();
        }

        var totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        businesses = businesses
           .Skip((request.PageIndex - 1) * request.PageSize)
           .Take(request.PageSize)
           .ToList();

        return (businesses, totalPages, totalItems);
    }

    public async Task<(List<BusinessWaitingVerifyResponse>?, int, int)> SearchWaitingBusinessAsync(GetWaitingVerifyBussinessesQuery request)
    {
        var query = from business in _context.Businesses
                    join verification in _context.Verifications
                    on business.Id equals verification.BusinessId
                    where !business.IsVerified || !verification.IsChecked
                    select new
                    {
                        Business = business,
                        Verification = verification
                    };


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = HttpUtility.UrlDecode(request.SearchTerm); // Giải mã từ URL

            var searchTermNoDiacritics = StringHandlerUtil.RemoveDiacritics(searchTerm).ToLower();

            query = query.Where(bv => bv.Business.Name.ToLower().Contains(searchTermNoDiacritics));
        }

        query = request.newestSorting
         ? query.OrderByDescending(bv => bv.Business.CreatedDate)
         : query.OrderBy(bv => bv.Business.CreatedDate);


        var totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        var businesses = await query
        .Skip((request.PageIndex - 1) * request.PageSize)
        .Take(request.PageSize)
        .AsNoTracking()
        .Select(bv => new BusinessWaitingVerifyResponse(
            bv.Business.Id,
            bv.Business.TaxCode,
            bv.Business.Name,
            bv.Business.IsVerified,
            new VerificationResponse(
                bv.Verification.BusinessLicense,
                bv.Verification.EstablishmentCertificate,
                bv.Verification.Note,
                bv.Verification.BusinessType,
                bv.Verification.CreatedDate
            )))
        .ToListAsync();
        return (businesses, totalPages, totalItems);
    }


    public void Update(Business business)
    {
        _context.Businesses.Update(business);
    }


}
