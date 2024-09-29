namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity<Guid>
{
    public bool IsActive { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }
}
