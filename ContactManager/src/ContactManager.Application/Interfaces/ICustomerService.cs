namespace ContactManager.Application.Interfaces;

public interface ICustomerService
{
    Task Create(CustomerDto customerDto);

    Task Update(CustomerDto customerDto);

    Task Delete(Guid id);

    Task<CustomerDto> GetById(Guid id);

    Task<IEnumerable<CustomerDto>> GetAll();
}
