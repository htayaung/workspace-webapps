using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;

namespace Application.Announcement.Queries.GetAnnouncementsWithPagination;

public record GetPublicAnnouncementsWithPaginationQuery : IRequest<PaginatedList<AnnouncementDto>>
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}

public class GetPublicAnnouncementsWithPaginationQueryHandler : IRequestHandler<GetPublicAnnouncementsWithPaginationQuery, PaginatedList<AnnouncementDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPublicAnnouncementsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AnnouncementDto>> Handle(GetPublicAnnouncementsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Announcements
            .Where(x => x.IsActive && x.IsPublic)
            .OrderBy(x => x.Created)
            .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}