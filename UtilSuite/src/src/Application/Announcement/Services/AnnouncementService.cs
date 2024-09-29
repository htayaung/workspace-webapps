using Application.Announcement.Queries;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Announcement.Services;

public class AnnouncementService
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly Random _random = new();

    public AnnouncementService(IApplicationDbContext context, IUser user, IMapper mapper)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
    }

    public async Task<AnnouncementDto> GetShortLink(string token)
    {
        return await _context.ShortLinks
            .Where(x => x.IsActive && x.Token == token)
            .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
