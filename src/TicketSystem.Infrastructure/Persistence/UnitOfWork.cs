using TicketSystem.Application.Interfaces;

namespace TicketSystem.Infrastructure.Persistence;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}