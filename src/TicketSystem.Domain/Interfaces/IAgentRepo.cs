using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces;

public interface IAgentRepo : IDomainRepo<Agent>
{
    Task<Agent?> GetByUsernameAsync(string username);
    Task<ICollection<Agent>> GetAllAvailableAsync();
    Task<ICollection<Agent>> GetAllAsync();
}