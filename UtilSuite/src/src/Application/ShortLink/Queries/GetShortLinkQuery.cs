using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.ShortLink.Queries;

public record GetShortLinkQuery : IRequest<ShortLinkDto>
{
    public Guid Id { get; set; }
}

public class GetShortLinkQueryHandler
    : IRequestHandler<GetShortLinkQuery, ShortLinkDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetShortLinkQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<ShortLinkDto> Handle(
        GetShortLinkQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.ShortLinks
            .Where(x => x.IsActive && x.CreatedBy == _user.Id && x.Id == request.Id)
            .ProjectTo<ShortLinkDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
