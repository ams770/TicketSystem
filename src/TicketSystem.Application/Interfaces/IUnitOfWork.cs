namespace TicketSystem.Application.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}