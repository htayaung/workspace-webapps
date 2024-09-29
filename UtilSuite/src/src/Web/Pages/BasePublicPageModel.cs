using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class BasePublicPageModel : PageModel
    {
        private IMediator _mediator;
        private readonly ILogger<BasePublicPageModel> _logger;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public BasePublicPageModel(
            ILogger<BasePublicPageModel> logger)
        {
            _logger = logger;
        }

        protected void Run(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                ModelState.AddModelError(string.Empty, ex.Message);
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
