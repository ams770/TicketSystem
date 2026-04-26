using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces;

public interface IUserRepo : IDomainRepo<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<PagedResult<User>> GetAllAsync(SearchablePagedRequest request);
}