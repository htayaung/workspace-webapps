using Application.Announcement.Commands.UpdateAnnouncement;
using Application.Announcement.Queries;
using Application.Common.Interfaces;
using Application.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Web.Utility;

namespace Web.Pages.Announcements;

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
        public string Name { get; set; }

        [RequiredField]
        public string Description { get; set; }

        [RequiredField]
        public DateTime StartDate { get; set; }

        [RequiredField]
        public DateTime EndDate { get; set; }

        [RequiredField]
        public bool IsPublic { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var decodedId = HttpUtility.UrlDecode(id);
        var safeId = EncryptDecryptHelper.DecryptByAes(decodedId, UserId.ToString());
        var announcementDto = await Mediator.Send(new GetAnnouncementQuery
        {
            Id = Guid.Parse(safeId)
        });

        if (announcementDto == null)
        {
            return NotFound();
        }

        Input.Id = announcementDto.Id;
        Input.Name = announcementDto.Name;
        Input.Description = announcementDto.Description;
        Input.StartDate = announcementDto.StartDate;
        Input.EndDate = announcementDto.EndDate;
        Input.IsPublic = announcementDto.IsPublic;

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
            var announcementDto = await Mediator.Send(new GetAnnouncementQuery
            {
                Id = Input.Id
            });

            if (announcementDto == null)
            {
                return NotFound();
            }

            // Html Sanitizer
            var safeDescription = WebHelper.SanitizeHtml(Input.Description);

            await Mediator.Send(new UpdateAnnouncementCommand
            {
                Id = Input.Id,
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
