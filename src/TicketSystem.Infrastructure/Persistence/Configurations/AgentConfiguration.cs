using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence.Configurations;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Navigation(a => a.Tickets)
            .HasField("_tickets")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


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
    }
}