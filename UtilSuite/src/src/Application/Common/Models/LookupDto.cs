namespace Application.Common.Models;

public class LookupDto
{
    public Guid Id { get; init; }

    public string? Title { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Domain.Entities.ShortLink, LookupDto>();
        }
    }
}
