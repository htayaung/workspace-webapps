using Application.Announcement.Commands.CreateAnnouncement;
using Application.Announcement.Services;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Utility;

namespace Web.Pages.Announcements;

public class CreateModel : BasePageModel
{
    private readonly AnnouncementService _announcementService;

    public CreateModel(
        AnnouncementService announcementService,
        IUser user,
        ILogger<CreateModel> logger) : base(user, logger)
    {
        _announcementService = announcementService;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [RequiredField]
        public string Name { get; set; }

        [RequiredField]
        public string Description { get; set; }

        [RequiredField]
        public DateTime StartDate { get; set; }

        [RequiredField]
        public DateTime EndDate { get; set; }

        [RequiredField]
        [DisplayName("Is Public?")]
        public bool IsPublic { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return await RunAsync(async () =>
        {
            // Html Sanitizer
            var safeDescription = WebHelper.SanitizeHtml(Input.Description);

            await Mediator.Send(new CreateAnnouncementCommand
            {
                Name = Input.Name,
                Description = safeDescription,
                StartDate = Input.StartDate,
                EndDate = Input.EndDate,
                IsPublic = Input.IsPublic
            });

            return RedirectToPage("./Index");
        });
    }
}
