using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ContactManager.Domain.Common;

namespace ContactManager.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable queryable,
        IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();

    public static Task<TDestination> ProjectTo<TDestination>(this BaseAuditableEntity entity,
        IConfigurationProvider configuration) where TDestination : class => entity.ProjectTo<TDestination>(configuration);
}
