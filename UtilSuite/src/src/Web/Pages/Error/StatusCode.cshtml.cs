using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Error;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class StatusCodeModel : PageModel
{
    public new int StatusCode { get; set; }

    public void OnGet(int statusCode)
    {
        StatusCode = statusCode;
    }
}
