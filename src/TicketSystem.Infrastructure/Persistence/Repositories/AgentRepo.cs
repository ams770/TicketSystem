using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repositories;

public class AgentRepo(AppDbContext dbContext) : IAgentRepo
{
    public async Task AddAsync(Agent entity) =>
        await dbContext.Agents.AddAsync(entity);

    public async Task<Agent?> GetByIdAsync(Guid id) =>
        await dbContext.Agents.FindAsync(id);

    public async Task<Agent?> GetByUsernameAsync(string username) =>
        await dbContext.Agents
            .FirstOrDefaultAsync(a => a.Username == username);

    public async Task<ICollection<Agent>> GetAllAvailableAsync() =>
        await dbContext.Agents
            .Where(a => a.IsAvailable)
            .ToListAsync();

    public async Task<ICollection<Agent>> GetAllAsync() =>
        await dbContext.Agents.ToListAsync();

    public async Task SaveChangesAsync() =>
        await dbContext.SaveChangesAsync();
}