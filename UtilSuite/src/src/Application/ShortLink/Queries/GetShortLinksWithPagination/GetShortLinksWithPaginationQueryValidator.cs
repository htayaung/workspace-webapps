namespace Application.ShortLink.Queries.GetShortLinksWithPagination;

public class GetShortLinksWithPaginationQueryValidator : AbstractValidator<GetShortLinksWithPaginationQuery>
{
    public GetShortLinksWithPaginationQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("Invalid User Id");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}
