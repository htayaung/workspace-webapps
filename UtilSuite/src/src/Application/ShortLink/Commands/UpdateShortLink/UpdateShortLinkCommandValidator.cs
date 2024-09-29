namespace Application.ShortLink.Commands.UpdateShortLink;

public class UpdateShortLinkCommandValidator : AbstractValidator<UpdateShortLinkCommand>
{
    public UpdateShortLinkCommandValidator()
    {
        RuleFor(x => x.Url)
            .MaximumLength(3000)
            .NotEmpty();

        RuleFor(x => x.ShortenedUrl)
            .MaximumLength(1024)
            .NotEmpty();

        RuleFor(x => x.Token)
            .MaximumLength(16)
            .NotEmpty();
    }
}
