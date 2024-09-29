using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.Configurations;

public class ShortLinkConfiguration : IEntityTypeConfiguration<ShortLink>
{
    public void Configure(EntityTypeBuilder<ShortLink> builder)
    {
        builder.ToTable("ShortLink");

        builder.Property(x => x.Url)
            .HasMaxLength(3000)
            .IsRequired();

        builder.Property(x => x.ShortenedUrl)
            .HasMaxLength(1024)
            .IsRequired();

        builder.Property(x => x.Token)
            .HasMaxLength(16)
            .IsRequired();
    }
}
