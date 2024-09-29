using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTasks.Models;

namespace MyTasks.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<TaskTag> TaskTags { get; set; }

    public DbSet<TaskColumn> TaskColumns { get; set; }

    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Change identity table names
        modelBuilder.Entity<ApplicationUser>(x => x.ToTable("User"));
        modelBuilder.Entity<ApplicationRole>(x => x.ToTable("Role"));
        modelBuilder.Entity<IdentityUserClaim<Guid>>(x => x.ToTable("UserClaim"));
        modelBuilder.Entity<IdentityUserLogin<Guid>>(x => x.ToTable("UserLogin"));
        modelBuilder.Entity<IdentityUserToken<Guid>>(x => x.ToTable("UserToken"));
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(x => x.ToTable("RoleClaim"));
        modelBuilder.Entity<IdentityUserRole<Guid>>(x => x.ToTable("UserRole"));
    }
}
