using ContactManager.Domain.Common;

namespace ContactManager.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string CustomerId { get; set; }

    public string DisplayName { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string JobTitle { get; set; }

    public string Department { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }
}
