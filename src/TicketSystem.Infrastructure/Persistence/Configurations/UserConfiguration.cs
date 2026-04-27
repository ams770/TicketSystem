using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.Navigation(a => a.Tickets)
            .HasField("_tickets")
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired();
        
        builder.HasIndex(u => u.Username)
            .IsUnique(); // prevent duplication from the db itself
    }
}