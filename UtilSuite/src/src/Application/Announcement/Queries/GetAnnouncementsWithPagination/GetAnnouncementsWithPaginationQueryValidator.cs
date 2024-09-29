namespace Application.Announcement.Queries.GetAnnouncementsWithPagination;

public class GetAnnouncementsWithPaginationQueryValidator : AbstractValidator<GetAnnouncementsWithPaginationQuery>
{
    public GetAnnouncementsWithPaginationQueryValidator()
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
