using TicketSystem.Domain.Entities;

namespace TicketSystem.Domain.Interfaces;

public interface ICategoryRepo : IDomainRepo<Category>
{
    Task<ICollection<Category>> GetAllAsync();
}