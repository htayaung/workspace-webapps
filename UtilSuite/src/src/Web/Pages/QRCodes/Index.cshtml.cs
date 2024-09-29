using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.QRCodes;

public class IndexModel : BasePageModel
{
    public IndexModel(
        IUser user,
        ILogger<IndexModel> logger) : base(user, logger)
    {

    }

    [BindProperty]
    [RequiredField]
    public string Input { get; set; }

    public string QRCodeImage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (MemoryStream ms = new())
        {
            QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(PngByteQRCodeHelper.GetQRCode(Input, QRCodeGenerator.ECCLevel.Q, 10));
        }

        return Page();
    }
}
