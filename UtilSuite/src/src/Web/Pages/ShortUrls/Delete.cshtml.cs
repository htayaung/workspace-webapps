using Application.Common.Interfaces;
using Application.Common.Utility;
using Application.ShortLink.Commands.DeleteShortLink;
using Application.ShortLink.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Web;

namespace Web.Pages.ShortUrls;

public class DeleteModel : BasePageModel
{
    public DeleteModel(
       IUser user,
       ILogger<CreateModel> logger) : base(user, logger) { }

    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        [DisplayName("Shortened Url")]
        public string ShortenedUrl { get; set; }

        public int Clicked { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        return await RunAsync(async () =>
        {
            if (id == null)
            {
                return NotFound();
            }

            var decodedUrl = HttpUtility.UrlDecode(id);
            var safeId = EncryptDecryptHelper.DecryptByAes(decodedUrl, UserId.ToString());
            var shortLinkDto = await Mediator.Send(new GetShortLinkQuery
            {
                Id = Guid.Parse(safeId)
            });

            if (shortLinkDto == null)
            {
                return NotFound();
            }
            else
            {
                Input.Id = shortLinkDto.Id;
                Input.Url = HttpUtility.UrlDecode(shortLinkDto.Url);
                Input.ShortenedUrl = shortLinkDto.ShortenedUrl;
                Input.Clicked = shortLinkDto.Clicked;
            }

            return Page();
        });
    }

    public async Task<IActionResult> OnPostAsync(string? id)
    {
        return await RunAsync(async () =>
        {
            if (id == null)
            {
                return NotFound();
            }

            var decodedUrl = HttpUtility.UrlDecode(id);
            var safeId = EncryptDecryptHelper.DecryptByAes(decodedUrl, UserId.ToString());
            var shortLinkDto = await Mediator.Send(new GetShortLinkQuery
            {
                Id = Guid.Parse(safeId)
            });

            if (shortLinkDto == null)
            {
                return NotFound();
            }

            await Mediator.Send(new DeleteShortLinkCommand(shortLinkDto.Id));

            return RedirectToPage("./Index");
        });
    }
}
