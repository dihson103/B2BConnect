﻿using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class IndustryRepository : IIndustryRepository
{
    private readonly AppDbContext _context;

    public IndustryRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRange(List<Industry> industries)
    {
        _context.Industries.AddRange(industries);
    }

    public async Task<bool> IsAllIndustryIdsExistAsync(List<Guid> industryIds)
    {
        if (industryIds is null || industryIds.Count == 0)
        {
            return false;
        }

        var numberIndustry = await _context.Industries.CountAsync(i => industryIds.Contains(i.Id));

        return numberIndustry == industryIds.Count();
    }

    public async Task<List<Industry>> SearchIndustrieAsync(string name)
    {
        var query = _context.Industries.AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(industry => industry.Name.ToLower().Contains(name.ToLower()));
        }

        return await query.ToListAsync();
    }

}
