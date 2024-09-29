using Application.Common.Utility;
using Application.ShortLink.Commands.UpdateShortLink;
using Application.ShortLink.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Web.Pages.Public
{
    public class RedirectLinkModel : BasePublicPageModel
    {
        private readonly UrlShortenerService _shortenerService;

        public RedirectLinkModel(
            UrlShortenerService shortenerService,
            ILogger<RedirectLinkModel> logger) : base(logger)
        {
            _shortenerService = shortenerService;
        }

        public async Task<IActionResult> OnGetAsync(string token)
        {
            if (token.IsNull())
            {
                return NotFound();
            }

            var shortLinkDto = await _shortenerService.GetShortLink(token);
            if (shortLinkDto == null)
            {
                return NotFound();
            }

            if (shortLinkDto.Url.IsNull())
            {
                return NotFound();
            }

            // Update clicked count
            await Mediator.Send(new UpdateShortLinkCommand
            {
                Id = shortLinkDto.Id,
                Url = shortLinkDto.Url,
                ShortenedUrl = shortLinkDto.ShortenedUrl,
                Token = shortLinkDto.Token,
                Clicked = shortLinkDto.Clicked + 1
            });

            var url = HttpUtility.UrlDecode(shortLinkDto.Url);
            return Redirect(url);
        }
    }
}
