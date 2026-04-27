using System.Security.Claims;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(Guid userId, string email, string role);
}