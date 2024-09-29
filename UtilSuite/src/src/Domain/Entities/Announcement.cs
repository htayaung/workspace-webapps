namespace Domain.Entities;

public class Announcement : BaseAuditableEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool IsPublic { get; set; }
}
