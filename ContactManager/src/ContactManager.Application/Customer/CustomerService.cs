using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContactManager.Application.Common.Exceptions;
using ContactManager.Application.Common.Mappings;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using ContactManager.Domain.Repositories;

namespace ContactManager.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repository;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Create(CustomerDto customerDto)
    {
        var customer = _mapper.Map<Customer>(customerDto);
        await _repository.AddAsync(customer);
    }

    public async Task Delete(Guid id)
    {
        await _repository.DeleteByIdAsync(id);
    }

    public Task<IEnumerable<CustomerDto>> GetAll()
    {
        return Task.Run(() => _repository.GetAll().ProjectTo<CustomerDto>(_mapper.ConfigurationProvider).AsEnumerable());
    }

    public async Task<CustomerDto> GetById(Guid id)
    {
        var customer = await _repository.GetByIdAsync(id);
        return _mapper.Map<CustomerDto>(customer);
    }

    public async Task Update(CustomerDto customerDto)
    {
        var customer = await _repository.GetByIdAsync(customerDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        customer.DisplayName = customerDto.DisplayName;
        customer.FullName = customerDto.FullName;
        customer.Email = customerDto.Email;
        customer.JobTitle = customerDto.JobTitle;
        customer.Department = customerDto.Department;
        customer.WorkPhone = customerDto.WorkPhone;
        customer.MobilePhone = customerDto.MobilePhone;

        await _repository.UpdateAsync(customer);
    }
}
