using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public class User : BaseActor
{
    private User()
    {
    }


    private readonly List<Ticket> _tickets = new();
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();


    public static User Create(string fullName, string email, string passwordHash)
    {
        ValidateFullName(fullName);
        ValidateEmail(email);

        return new User
        {
            Id = Guid.NewGuid(),
            FullName = fullName.Trim(),
            Email = email.Trim().ToLower(),
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };
    }
}