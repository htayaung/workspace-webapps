namespace Application.ShortLink.Commands.CreateShortLink;

public class CreateShortLinkCommandValidator : AbstractValidator<CreateShortLinkCommand>
{
    public CreateShortLinkCommandValidator()
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
