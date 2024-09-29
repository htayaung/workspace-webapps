using Ardalis.GuardClauses;
using Application.Common.Interfaces;
using Domain.Events;

namespace Application.ShortLink.Commands.DeleteShortLink;

public record DeleteShortLinkCommand(Guid id) : IRequest;

public class DeleteShortLinkCommandHandler : IRequestHandler<DeleteShortLinkCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteShortLinkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteShortLinkCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .ShortLinks
            .FindAsync(new object[] { request.id }, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        entity.IsActive = false;
        entity.AddDomainEvent(new ShortLinkDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
