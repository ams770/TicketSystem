using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces;

public interface ITicketRepo : IDomainRepo<Ticket>
{
    Task<PagedResult<Ticket>> GetAllAsync(TicketPagedRequest request);
}