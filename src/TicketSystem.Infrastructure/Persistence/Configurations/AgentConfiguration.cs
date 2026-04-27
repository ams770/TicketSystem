using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.FullName)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(a => a.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.PasswordHash)
            .IsRequired();

        builder.HasIndex(a => a.Username)
            .IsUnique();
        
        // ── Seed Data ──────────────────────────────────────
        // BCrypt hash of "Admin@1234" — generated once, never changes
        // var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd");
        //
        // builder.HasData(new
        // {
        //     Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
        //     FullName = "System Admin",
        //     Username = "admin",
        //     PasswordHash = adminPasswordHash,
        //     IsAvailable = true,
        //     CreatedAt = DateTime.UtcNow
        // });
    }
}