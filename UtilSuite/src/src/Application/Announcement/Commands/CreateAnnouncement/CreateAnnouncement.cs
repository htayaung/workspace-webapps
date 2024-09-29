using Application.Common.Interfaces;
using Domain.Events;

namespace Application.Announcement.Commands.CreateAnnouncement;

public record CreateAnnouncementCommand : IRequest<Guid>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublic { get; set; }
}

public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAnnouncementCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Announcement
        {
            Name = request.Name,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsPublic = request.IsPublic
        };

        entity.AddDomainEvent(new AnnouncementCreatedEvent(entity));
        _context.Announcements.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
