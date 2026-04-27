using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Commands.ChangeTicketStatus;

public class ChangeTicketStatusService(ITicketRepo ticketRepo, IAgentRepo agentRepo)
{
    public async Task<ChangeTicketStatusResult> ExecuteAsync(ChangeTicketStatusCommand command)
    {
        var ticket = await ticketRepo.GetByIdAsync(command.TicketId) ??
                     throw new NotFoundException(nameof(Ticket), command.TicketId);
        // Release the Agent
        if (command.Status == TicketStatus.Resolved && ticket.AgentId.HasValue)
        {
            var agent = await agentRepo.GetByIdAsync(ticket.AgentId.Value)
                        ?? throw new NotFoundException(nameof(Agent), ticket.AgentId.Value);
            agent.SetAvailability(true);
        }
        
        // Update the ticket status
        ticket.ChangeStatus(command.Status);
        
        // Save all changes
        await ticketRepo.SaveChangesAsync();
        
        return new ChangeTicketStatusResult
        {
            TicketId = command.TicketId,
            Status = command.Status.ToString()
        };
    }
}