using Ardalis.GuardClauses;
using Application.Common.Interfaces;

namespace Application.Announcement.Commands.UpdateAnnouncement;

public record UpdateAnnouncementCommand : IRequest
{
    public Guid Id { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublic { get; set; }
}

public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAnnouncementCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context
            .Announcements
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.IsPublic = request.IsPublic;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
