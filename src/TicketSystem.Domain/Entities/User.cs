using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public class User : BaseActor
{
    private User()
    {
    }


    private readonly List<Ticket> _tickets = new();
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();


    public static User Create(string fullName, string username, string passwordHash)
    {
        ValidateFullName(fullName);
        ValidateUsername(username);

        return new User
        {
            Id = Guid.NewGuid(),
            FullName = fullName.Trim(),
            Username = username.Trim().ToLower(),
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };
    }
}