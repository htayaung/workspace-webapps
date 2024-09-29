using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactManager.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customer");

        builder.Property(x => x.CustomerId)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(x => x.DisplayName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.FullName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.JobTitle)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Department)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.WorkPhone)
            .HasMaxLength(128);

        builder.Property(x => x.MobilePhone)
            .HasMaxLength(128);
    }
}
