using ContactManager.Application.Common.Interfaces;
using ContactManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ContactManager.Web.Controllers;

[Route("api/[controller]")]
public class CustomersController : ApiControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(
        IUser user,
        ILogger<CustomersController> logger,
        ICustomerService customerService) : base(user, logger)
    {
        _customerService = customerService;
    }

    // GET: api/customers
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCustomers()
    {
        return await RunAsync(async () =>
        {
            var result = await _customerService.GetAll();
            return Ok(result);
        });
    }

    // GET: api/customers/1F9EA805-063C-4F15-AA15-3369B35C53E9
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        return await RunAsync(async () =>
        {
            var result = await _customerService.GetById(id);
            if (result == null)
            {
                throw new NotFoundException();
            }

            return Ok(result);
        });
    }

    // POST: api/customers
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerDto customer)
    {
        return await RunAsync(async () =>
        {
            await _customerService.Create(customer);
            return Created();
        });
    }

    // PUT: api/customers
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto customer)
    {
        return await RunAsync(async () =>
        {
            await _customerService.Update(customer);
            return Ok();
        });
    }

    // DELETE: api/customers/1F9EA805-063C-4F15-AA15-3369B35C53E9
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        return await RunAsync(async () =>
        {
            await _customerService.Delete(id);
            return NoContent();
        });
    }
}
