using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Data;

namespace ContactManager.Infrastructure.Repositories;

public class CustomerRepository
    : GenericRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context)
        : base(context)
    { }
}
