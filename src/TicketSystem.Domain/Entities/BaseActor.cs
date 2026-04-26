using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public abstract class BaseActor
{
    public Guid Id { get; protected set; }
    public string FullName { get; protected set; } = null!;
    public string Email { get; protected set; } = null!;
    protected string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; protected set; }


    public void SetFullName(string fullName)
    {
        ValidateFullName(fullName);
        FullName = fullName.Trim();
    }

    public void SetEmail(string email)
    {
        ValidateEmail(email);
        Email = email.Trim();
    }

    public bool CheckPassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainException("Password hash is required.");

        return PasswordHash == passwordHash;
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

    protected static void ValidateEmail(string email)
    {
        // todo replace this check with regex
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new DomainException("A valid email is required");
    }
}