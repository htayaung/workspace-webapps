using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class BasePageModel : PageModel
    {
        private IMediator _mediator;
        private readonly IUser _user;
        private readonly ILogger<BasePageModel> _logger;

        [BindProperty]
        public Guid UserId
        {
            get
            {
                return _user.Id;
            }
        }

        protected int DefaultPageSize
        {
            get
            {
                return 10;
            }
        }

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public BasePageModel(
            IUser user,
            ILogger<BasePageModel> logger)
        {
            _user = user;
            _logger = logger;
        }

        protected IActionResult Run(Func<IActionResult> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        protected async Task<IActionResult> RunAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
