using Application.Common.Mappings;

namespace Application.Announcement.Queries;

public class AnnouncementDto : IMapFrom<Domain.Entities.Announcement>
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublic { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUpdatedDate
    {
        get
        {
            return LastModified ?? Created;
        }
    }
}
