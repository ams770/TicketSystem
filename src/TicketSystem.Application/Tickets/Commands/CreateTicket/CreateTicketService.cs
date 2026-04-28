using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Commands.CreateTicket;

public class CreateTicketService(IUnitOfWork unitOfWork, ITicketRepo ticketRepo, IUserRepo userRepo, ICategoryRepo categoryRepo)
{
    public async Task<CreateTicketResult> ExecuteAsync(CreateTicketCommand command)
    {
        // Check user exists
        _ = await userRepo.GetByIdAsync(command.UserId)
            ?? throw new NotFoundException(nameof(User), command.UserId);
        // Check category exists
        _ = await categoryRepo.GetByIdAsync(command.CategoryId) ??
            throw new NotFoundException(nameof(Category), command.CategoryId);
        // Ask Domain to create the ticket
        var createdTicket = Ticket.Create(
            title: command.Title,
            description: command.Description,
            categoryId: command.CategoryId,
            userId: command.UserId,
            priority: command.Priority
        );

        await ticketRepo.AddAsync(createdTicket);
        await unitOfWork.SaveChangesAsync();

        return new CreateTicketResult
        {
            Id = createdTicket.Id,
            CreatedAt = createdTicket.CreatedAt,
            Priority = createdTicket.Priority.ToString(),
            Status = createdTicket.Status.ToString(),
        };
    }
}