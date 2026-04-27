using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public abstract class BaseActor
{
    public Guid Id { get; protected set; }
    public string FullName { get; protected set; } = null!;
    public string Username { get; protected set; } = null!;
    public string PasswordHash { get; protected set; } = null!;
    public DateTime CreatedAt { get; protected set; }


    public void SetFullName(string fullName)
    {
        ValidateFullName(fullName);
        FullName = fullName.Trim();
    }

    public void SetUsername(string username)
    {
        ValidateUsername(username);
        Username = username.Trim();
    }



    public void ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new DomainException("Password hash is required.");

        PasswordHash = newPasswordHash;
    }


    protected static void ValidateFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new DomainException("Full name is required.");
    }

    protected static void ValidateUsername(string username)
    {
        // todo replace this check with regex
        if (string.IsNullOrWhiteSpace(username) || username.Length < 6)
            throw new DomainException("A valid Username is required");
    }
}