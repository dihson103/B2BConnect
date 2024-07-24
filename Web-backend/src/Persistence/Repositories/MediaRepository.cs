using Application.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal class MediaRepository : IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context)
    {
        _context = context;
    }

    public void AddRange(List<Media> medias)
    {
        _context.Medias.AddRange(medias);
    }

    public async Task<bool> IsAllMediasExistAsync(List<Guid> Ids)
    {
        var count = await _context.Medias.CountAsync(m => Ids.Contains(m.Id));

        return count == Ids.Count;
    }
}
