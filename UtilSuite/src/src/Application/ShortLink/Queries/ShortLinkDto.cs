using Application.Common.Mappings;

namespace Application.ShortLink.Queries;

public class ShortLinkDto : IMapFrom<Domain.Entities.ShortLink>
{
    public Guid Id { get; set; }

    public string Url { get; set; }

    public string ShortenedUrl { get; set; }

    public string Token { get; set; }

    public int Clicked { get; set; }

    public DateTime Created { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? LastModified { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime LastUpdatedDate
    {
        get
        {
            return LastModified ?? Created;
        }
    }
}
