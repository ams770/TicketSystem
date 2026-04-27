using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces;

public interface IUserRepo : IDomainRepo<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<PagedResult<User>> GetAllAsync(SearchablePagedRequest request);
}