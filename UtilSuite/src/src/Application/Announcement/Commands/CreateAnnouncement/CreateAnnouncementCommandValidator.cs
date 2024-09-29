namespace Application.Announcement.Commands.CreateAnnouncement;

public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementCommandValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(1024)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(4000)
            .NotEmpty();

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .NotEmpty();
    }
}
