using Application.Common.Interfaces;
using Application.ShortLink.Queries;
using AutoMapper.QueryableExtensions;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.ShortLink.Services;

public class UrlShortenerService
{
    private const int Length = 7;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private readonly IApplicationDbContext _context;
    private readonly IUser _user;
    private readonly IMapper _mapper;
    private readonly Random _random = new();

    public UrlShortenerService(IApplicationDbContext context, IUser user, IMapper mapper)
    {
        _context = context;
        _user = user;
        _mapper = mapper;
    }

    public async Task<string> GenerateToken()
    {
        var tokenChars = new char[Length];
        var maxLength = Alphabet.Length;

        while (true)
        {
            for (var i = 0; i < Length; i++)
            {
                var randomIndex = _random.Next(maxLength);
                tokenChars[i] = Alphabet[randomIndex];
            }

            var token = new string(tokenChars);

            if (!await _context.ShortLinks.AnyAsync(x => x.Token == token))
            {
                return token;
            }
        }
    }

    public async Task ValidateExistingUrl(string url)
    {
        if (await _context.ShortLinks.AnyAsync(x => x.CreatedBy == _user.Id && x.Url == url))
        {
            throw new AppException("URL already exist.");
        }

        return;
    }

    public async Task<ShortLinkDto> GetShortLink(string token)
    {
        return await _context.ShortLinks
            .Where(x => x.IsActive && x.Token == token)
            .ProjectTo<ShortLinkDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
