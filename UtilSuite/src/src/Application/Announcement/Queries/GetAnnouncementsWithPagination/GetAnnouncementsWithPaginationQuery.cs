using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Utility;
using AutoMapper.QueryableExtensions;

namespace Application.Announcement.Queries.GetAnnouncementsWithPagination;

public record GetAnnouncementsWithPaginationQuery : IRequest<PaginatedList<AnnouncementDto>>
{
    public Guid UserId { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public string? SearchString { get; set; }
}

public class GetAnnouncementsWithPaginationQueryHandler : IRequestHandler<GetAnnouncementsWithPaginationQuery, PaginatedList<AnnouncementDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetAnnouncementsWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AnnouncementDto>> Handle(GetAnnouncementsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.SearchString.IsNull())
        {
            return await _context.Announcements
                .Where(x => x.CreatedBy == request.UserId && x.IsActive)
                .OrderBy(x => x.Created)
                .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return await _context.Announcements
            .Where(x => x.CreatedBy == request.UserId && x.IsActive && (x.Name.Contains(request.SearchString)))
            .OrderBy(x => x.Created)
            .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}