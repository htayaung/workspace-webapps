using ContactManager.Application.Common.Interfaces;
using System.Net;

namespace Microsoft.AspNetCore.Mvc;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private readonly IUser _user;
    private readonly ILogger<ApiControllerBase> _logger;

    public ApiControllerBase(
        IUser user,
        ILogger<ApiControllerBase> logger)
    {
        _user = user;
        _logger = logger;
    }

    protected async Task<IActionResult> RunAsync(Func<Task<IActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            switch (ex)
            {
                case NotFoundException:
                case Ardalis.GuardClauses.NotFoundException:
                    _logger.LogError(ex.ToString());
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return StatusCode((int)HttpStatusCode.NotFound);
                default:
                    _logger.LogError(ex.ToString());
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
