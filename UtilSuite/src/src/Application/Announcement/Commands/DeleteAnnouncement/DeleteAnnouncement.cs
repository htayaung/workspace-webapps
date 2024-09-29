using Ardalis.GuardClauses;
using Application.Common.Interfaces;
using Domain.Events;

namespace Application.Announcement.Commands.DeleteAnnouncement;

public record DeleteAnnouncementCommand(Guid id) : IRequest;

public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAnnouncementCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Announcements
            .FindAsync(new object[] { request.id }, cancellationToken);

        Guard.Against.NotFound(request.id, entity);

        entity.IsActive = false;
        entity.AddDomainEvent(new AnnouncementDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }
}
