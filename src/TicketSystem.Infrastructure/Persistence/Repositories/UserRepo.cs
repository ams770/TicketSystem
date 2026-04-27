using Microsoft.EntityFrameworkCore;
using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repositories;

public class UserRepo(AppDbContext dbContext) : IUserRepo
{
    public async Task AddAsync(User entity) =>
        await dbContext.Users.AddAsync(entity);

    public async Task<User?> GetByIdAsync(Guid id) =>
        await dbContext.Users.FindAsync(id);

    public async Task<User?> GetByUsernameAsync(string username) =>
        await dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username);

    public async Task<PagedResult<User>> GetAllAsync(SearchablePagedRequest request)
    {
        var query = dbContext.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            query = query.Where(u =>
                u.FullName.Contains(request.SearchTerm) ||
                u.Username.Contains(request.SearchTerm));

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<User>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    public async Task SaveChangesAsync() =>
        await dbContext.SaveChangesAsync();
}