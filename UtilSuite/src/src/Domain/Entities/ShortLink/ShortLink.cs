namespace Domain.Entities;

public class ShortLink : BaseAuditableEntity
{
    public string Url { get; set; }

    public string ShortenedUrl { get; set; }

    public string Token { get; set; }

    public int Clicked { get; set; }
}
