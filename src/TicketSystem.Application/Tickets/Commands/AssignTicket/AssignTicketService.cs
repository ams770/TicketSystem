using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Commands.AssignTicket;

public class AssignTicketService(IUnitOfWork unitOfWork, ITicketRepo ticketRepo, IAgentRepo agentRepo)
{
    public async Task<AssignTicketResult> ExecuteAsync(AssignTicketCommand command)
    {
        // -
        var agent = await agentRepo.GetByIdAsync(command.AgentId) ??
                    throw new NotFoundException(nameof(Agent), command.AgentId);
        // -
        var ticket = await ticketRepo.GetByIdAsync(command.TicketId) ??
                     throw new NotFoundException(nameof(Ticket), command.TicketId);
        // -
        ticket.AssignAgent(agent);
        
        await unitOfWork.SaveChangesAsync();

        return new AssignTicketResult
        {
            TicketId = command.TicketId,
            AgentId = command.AgentId,
            Status = ticket.Status.ToString(),
        };
    }
}