using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Announcement.Queries;

public record GetAnnouncementQuery : IRequest<AnnouncementDto>
{
    public Guid Id { get; set; }
}

public class GetAnnouncementQueryHandler
    : IRequestHandler<GetAnnouncementQuery, AnnouncementDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetAnnouncementQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<AnnouncementDto> Handle(
        GetAnnouncementQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Announcements
            .Where(x => x.IsActive && x.CreatedBy == _user.Id && x.Id == request.Id)
            .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
