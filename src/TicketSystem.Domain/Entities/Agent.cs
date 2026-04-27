using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public class Agent : BaseActor
{
    private Agent()
    {
    }

    public bool IsAvailable { get; private set; }

    private readonly List<Ticket> _tickets = [];
    
    public IReadOnlyCollection<Ticket> Tickets => _tickets.AsReadOnly();


    public static Agent Create(string fullName, string username, string passwordHash)
    {
        ValidateFullName(fullName);
        ValidateUsername(username);

        return new Agent
        {
            Id = Guid.NewGuid(),
            FullName = fullName.Trim(),
            Username = username.Trim().ToLower(),
            PasswordHash = passwordHash,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };
    }


    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}