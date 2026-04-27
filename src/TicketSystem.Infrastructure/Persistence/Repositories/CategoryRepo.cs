using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repositories;

public class CategoryRepo(AppDbContext dbContext) : ICategoryRepo
{
    public async Task AddAsync(Category entity) =>
        await dbContext.Categories.AddAsync(entity);

    public async Task<Category?> GetByIdAsync(Guid id) =>
        await dbContext.Categories.FindAsync(id);

    public async Task<ICollection<Category>> GetAllAsync() =>
        await dbContext.Categories.ToListAsync();

    public async Task SaveChangesAsync() =>
        await dbContext.SaveChangesAsync();
}