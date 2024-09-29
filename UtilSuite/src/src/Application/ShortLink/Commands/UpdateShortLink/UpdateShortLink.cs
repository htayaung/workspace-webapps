using Ardalis.GuardClauses;
using Application.Common.Interfaces;

namespace Application.ShortLink.Commands.UpdateShortLink;

public record UpdateShortLinkCommand : IRequest
{
    public Guid Id { get; init; }

    public string Url { get; init; }

    public string ShortenedUrl { get; init; }

    public string Token { get; init; }

    public int Clicked { get; set; }
}

public class UpdateShortLinkCommandHandler : IRequestHandler<UpdateShortLinkCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateShortLinkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateShortLinkCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .ShortLinks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Url = request.Url;
        entity.ShortenedUrl = request.ShortenedUrl;
        entity.Token = request.Token;
        entity.Clicked = request.Clicked;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
