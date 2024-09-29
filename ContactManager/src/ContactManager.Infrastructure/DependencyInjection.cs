using Ardalis.GuardClauses;
using ContactManager.Infrastructure.Data.Interceptors;
using ContactManager.Infrastructure.Data;
using ContactManager.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ContactManager.Domain.Repositories;
using ContactManager.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()!);
            options.UseSqlServer(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton(typeof(ContactManager.Infrastructure.TimeProvider));
        services.AddAuthorization();

        return services;
    }
}
