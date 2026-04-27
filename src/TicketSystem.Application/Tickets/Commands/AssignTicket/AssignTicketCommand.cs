namespace TicketSystem.Application.Tickets.Commands.AssignTicket;

public class AssignTicketCommand
{
    public Guid TicketId { get; set; }
    public Guid AgentId { get; set; }
}