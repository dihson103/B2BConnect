using Application.Abstractions.Data;
using Domain.Entities;

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
}
