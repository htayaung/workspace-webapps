#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using Application.Announcement.Queries;
using Application.Announcement.Queries.GetAnnouncementsWithPagination;
using Application.Common.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.Account;
public class LoginModel : BasePublicPageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;

    public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger)
        : base(logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    [BindProperty(SupportsGet = true)]
    public InputModel Input { get; set; }

    public PaginatedList<AnnouncementDto> List { get; set; }

    public bool HasData
    {
        get
        {
            return List != null && List.TotalCount > 0;
        }
    }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(int? anIndex, string returnUrl = null)
    {
        return await RunAsync(async () =>
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            List = await Mediator.Send(new GetPublicAnnouncementsWithPaginationQuery
            {
                PageNumber = anIndex ?? 1,
                PageSize = int.MaxValue,
            });

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;

            return Page();
        });
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = false });
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "The email or password is incorrect.");
                return Page();
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }

    public async Task<IActionResult> OnPostLoginAsAdministrator(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        var result = await _signInManager.PasswordSignInAsync("administrator@htayaung.dev", "Admin@54637", false, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            _logger.LogInformation("User logged in.");
            return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = false });
        }
        if (result.IsLockedOut)
        {
            _logger.LogWarning("User account locked out.");
            return RedirectToPage("./Lockout");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "The email or password is incorrect.");
            return Page();
        }
    }
}
