using Application.Common.Interfaces;
using Application.Common.Models;
using Application.ShortLink.Queries;
using Application.ShortLink.Queries.GetShortLinksWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.ShortUrls;

public class IndexModel : BasePageModel
{
    public IndexModel(
        IUser user,
        ILogger<IndexModel> logger) : base(user, logger) { }

    [BindProperty]
    public PaginatedList<ShortLinkDto> List { get; set; }

    public string CurrentFilter { get; set; }

    public int PageSize { get; set; }

    public bool HasData
    {
        get
        {
            return List != null && List.TotalCount > 0;
        }
    }

    public async Task<IActionResult> OnGetAsync(int? pageIndex, string? searchString)
    {
        if (searchString != null)
        {
            pageIndex = 1;
            CurrentFilter = searchString;
        }

        PageSize = DefaultPageSize;
        return await RunAsync(async () =>
        {
            List = await Mediator.Send(new GetShortLinksWithPaginationQuery
            {
                UserId = UserId,
                PageNumber = pageIndex ?? 1,
                PageSize = PageSize,
                SearchString = searchString
            });

            return Page();
        });
    }
}
