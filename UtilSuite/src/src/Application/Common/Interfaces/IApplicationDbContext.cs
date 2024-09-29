using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.ShortLink> ShortLinks { get; }

    DbSet<Domain.Entities.Announcement> Announcements { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
