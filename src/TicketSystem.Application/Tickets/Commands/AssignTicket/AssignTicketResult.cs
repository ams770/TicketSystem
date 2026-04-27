namespace TicketSystem.Application.Tickets.Commands.AssignTicket;

public class AssignTicketResult
{
    public Guid TicketId { get; set; }
    public Guid AgentId { get; set; }
    public string Status { get; set; }= string.Empty;
}