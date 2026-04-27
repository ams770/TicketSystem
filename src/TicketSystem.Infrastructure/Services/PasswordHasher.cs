using TicketSystem.Application.Interfaces;

namespace TicketSystem.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) =>
        BCrypt.Net.BCrypt.HashPassword(password);
    
    public bool Verify(string password, string storedHash) =>
        BCrypt.Net.BCrypt.Verify(password, storedHash);
}