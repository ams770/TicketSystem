using Microsoft.EntityFrameworkCore;
using TicketSystem.Application.Tickets.Queries.GetAllTickets;
using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Infrastructure.Persistence.Repositories;


public class TicketRepo(AppDbContext dbContext) : ITicketRepo
{

    public async Task AddAsync(Ticket ticket) =>
        await dbContext.Tickets.AddAsync(ticket);

    public async Task<Ticket?> GetByIdAsync(Guid id) =>
        await dbContext.Tickets
            .Include(t => t.User)
            .Include(t => t.Agent)
            .Include(t => t.Category)
            .Include(t => t.Comments)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<PagedResult<Ticket>> GetAllAsync(TicketPagedRequest request)
    {
        var q = dbContext.Tickets
            .Include(t => t.User)
            .Include(t => t.Agent)
            .Include(t => t.Category)
            .AsQueryable();

        if (request.Status.HasValue)
            q = q.Where(t => t.Status == request.Status.Value);

        if (request.Priority.HasValue)
            q = q.Where(t => t.Priority == request.Priority.Value);

        if (request.AgentId.HasValue)
            q = q.Where(t => t.AgentId == request.AgentId.Value);

        if (request.CategoryId.HasValue)
            q = q.Where(t => t.CategoryId == request.CategoryId.Value);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            q = q.Where(t => t.Title.Contains(request.SearchTerm));

        var totalCount = await q.CountAsync();
        var items = await q
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Ticket>
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