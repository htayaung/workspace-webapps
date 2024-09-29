using Application.Common.Interfaces;
using Application.Common.Utility;
using Application.ShortLink.Commands.UpdateShortLink;
using Application.ShortLink.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Web.Pages.ShortUrls;

public class EditModel : BasePageModel
{
    public EditModel(
        IUser user,
        ILogger<CreateModel> logger) : base(user, logger) { }

    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public Guid Id { get; set; }

        [RequiredField]
        public string Url { get; set; }

        [DisplayName("Shortened Url")]
        public string ShortenedUrl { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var decodedId = HttpUtility.UrlDecode(id);
        var safeId = EncryptDecryptHelper.DecryptByAes(decodedId, UserId.ToString());
        var shortLinkDto = await Mediator.Send(new GetShortLinkQuery
        {
            Id = Guid.Parse(safeId)
        });

        if (shortLinkDto == null)
        {
            return NotFound();
        }

        Input.Id = shortLinkDto.Id;
        Input.Url = HttpUtility.UrlDecode(shortLinkDto.Url);
        Input.ShortenedUrl = shortLinkDto.ShortenedUrl;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return await RunAsync(async () =>
        {
            // TODO: Validate raw URL

            var shortLinkDto = await Mediator.Send(new GetShortLinkQuery
            {
                Id = Input.Id
            });

            if (shortLinkDto == null)
            {
                return NotFound();
            }

            // Encode URL
            var encodedUrl = HttpUtility.UrlEncode(Input.Url);

            await Mediator.Send(new UpdateShortLinkCommand
            {
                Id = Input.Id,
                Url = encodedUrl,
                ShortenedUrl = shortLinkDto.ShortenedUrl,
                Token = shortLinkDto.Token,
                Clicked = shortLinkDto.Clicked
            });

            return RedirectToPage("./Index");
        });
    }
}
