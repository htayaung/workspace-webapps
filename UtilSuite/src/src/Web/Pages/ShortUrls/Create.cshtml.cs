using Application.Common.Interfaces;
using Application.ShortLink.Commands.CreateShortLink;
using Application.ShortLink.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Web.Pages.ShortUrls;

public class CreateModel : BasePageModel
{
    private readonly UrlShortenerService _shortenerService;

    public CreateModel(
        UrlShortenerService shortenerService,
        IUser user,
        ILogger<CreateModel> logger) : base(user, logger)
    {
        _shortenerService = shortenerService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [RequiredField]
        public string Url { get; set; }
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return await RunAsync(async () =>
        {
            // Encode URL
            var encodedUrl = HttpUtility.UrlEncode(Input.Url);

            // Validate existing URL
            await _shortenerService.ValidateExistingUrl(encodedUrl);
            var token = await _shortenerService.GenerateToken();

            await Mediator.Send(new CreateShortLinkCommand
            {
                Url = encodedUrl,
                ShortenedUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/aka/{token}",
                Token = token
            });

            return RedirectToPage("./Index");
        });
    }
}
