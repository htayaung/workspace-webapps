using Application.Announcement.Commands.DeleteAnnouncement;
using Application.Announcement.Queries;
using Application.Common.Interfaces;
using Application.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Web;

namespace Web.Pages.Announcements;

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

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Is Public")]
        public bool IsPublic { get; set; }
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
            var announcementDto = await Mediator.Send(new GetAnnouncementQuery
            {
                Id = Guid.Parse(safeId)
            });

            if (announcementDto == null)
            {
                return NotFound();
            }
            else
            {
                Input.Id = announcementDto.Id;
                Input.Name = announcementDto.Name;
                Input.Description = announcementDto.Description;
                Input.StartDate = announcementDto.StartDate;
                Input.EndDate = announcementDto.EndDate;
                Input.IsPublic = announcementDto.IsPublic;
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
            var announcementDto = await Mediator.Send(new GetAnnouncementQuery
            {
                Id = Guid.Parse(safeId)
            });

            if (announcementDto == null)
            {
                return NotFound();
            }

            await Mediator.Send(new DeleteAnnouncementCommand(announcementDto.Id));

            return RedirectToPage("./Index");
        });
    }
}
