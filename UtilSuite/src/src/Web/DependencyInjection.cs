using Application.Common.Interfaces;
using Infrastructure.Data;
using Web.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddScoped<IUser, CurrentUser>();
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizeFolder("/ShortUrls");
            options.Conventions.AuthorizeFolder("/Announcements");
        });

        services.AddAntiforgery(option => option.HeaderName = "XSRF-TOKEN");

        return services;
    }
}
