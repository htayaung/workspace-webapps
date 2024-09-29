using Application.Common.Interfaces;
using Domain.Events;

namespace Application.ShortLink.Commands.CreateShortLink;

public record CreateShortLinkCommand : IRequest<Guid>
{
    public string Url { get; init; }

    public string ShortenedUrl { get; init; }

    public string Token { get; init; }

    public int Clicked { get; set; }
}

public class CreateShortLinkCommandHandler : IRequestHandler<CreateShortLinkCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateShortLinkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateShortLinkCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.ShortLink
        {
            Url = request.Url,
            ShortenedUrl = request.ShortenedUrl,
            Token = request.Token,
            Clicked = request.Clicked,
        };

        entity.AddDomainEvent(new ShortLinkCreatedEvent(entity));
        _context.ShortLinks.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
