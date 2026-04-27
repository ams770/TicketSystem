using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(c => c.Name)
            .IsUnique();
        
        
        // ── Seed Data ──────────────────────────────────────
        // builder.HasData(
        //     new { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "Technical Support" },
        //     new { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "Billing" },
        //     new { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "Account & Login" },
        //     new { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "General Inquiry" }
        // );
    }
}