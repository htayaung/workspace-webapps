using ContactManager.Application.Common.Mappings;
using ContactManager.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ContactManager.Application;

public class CustomerDto : IMapFrom<Customer>, IMapTo<Customer>
{
    public Guid Id { get; set; }

    public string CustomerId { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    public string FullName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string JobTitle { get; set; }

    [Required]
    public string Department { get; set; }

    public string WorkPhone { get; set; }

    public string MobilePhone { get; set; }
}
