using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Utility;
using AutoMapper.QueryableExtensions;

namespace Application.ShortLink.Queries.GetShortLinksWithPagination;

public record GetShortLinksWithPaginationQuery : IRequest<PaginatedList<ShortLinkDto>>
{
    public Guid UserId { get; init; }

    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;

    public string? SearchString { get; set; }
}

public class GetShortLinksWithPaginationQueryHandler : IRequestHandler<GetShortLinksWithPaginationQuery, PaginatedList<ShortLinkDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetShortLinksWithPaginationQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ShortLinkDto>> Handle(GetShortLinksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        if (request.SearchString.IsNull())
        {
            return await _context.ShortLinks
                .Where(x => x.CreatedBy == request.UserId && x.IsActive)
                .OrderBy(x => x.Created)
                .ProjectTo<ShortLinkDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }

        return await _context.ShortLinks
            .Where(x => x.CreatedBy == request.UserId && x.IsActive && (x.Url.Contains(request.SearchString)))
            .OrderBy(x => x.Created)
            .ProjectTo<ShortLinkDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}