using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Domain.Constants;
using Infrastructure.Identity;

namespace Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly TimeProvider _dateTime;

    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        TimeProvider dateTime)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _dateTime = dateTime;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var systemAdministratorRole = new ApplicationRole
        {
            Name = Roles.SystemAdministrator,
            IsActive = true,
            Created = _dateTime.GetUtcNow().DateTime,
            CreatedBy = Guid.Empty
        };

        if (_roleManager.Roles.All(x => x.Name != systemAdministratorRole.Name))
        {
            await _roleManager.CreateAsync(systemAdministratorRole);
        }

        var administratorRole = new ApplicationRole
        {
            Name = Roles.Administrator,
            IsActive = true,
            Created = _dateTime.GetUtcNow().DateTime,
            CreatedBy = Guid.Empty
        };

        if (_roleManager.Roles.All(x => x.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser
        {
            UserName = "administrator@htayaung.dev",
            Email = "administrator@htayaung.dev",
            DisplayName = "Administrator",
            IsActive = true,
            Created = _dateTime.GetUtcNow().DateTime,
            CreatedBy = Guid.Empty,
        };

        if (_userManager.Users.All(x => x.UserName != administrator.UserName))
        {
            await _userManager.SetUserNameAsync(administrator, administrator.Email);
            await _userManager.SetEmailAsync(administrator, administrator.Email);

            var result = await _userManager.CreateAsync(administrator, "Admin@54637");

            if (result.Succeeded && !string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }
    }
}
