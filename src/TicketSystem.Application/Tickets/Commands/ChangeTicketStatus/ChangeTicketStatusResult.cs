namespace TicketSystem.Application.Tickets.Commands.ChangeTicketStatus;

public class ChangeTicketStatusResult
{
    public Guid TicketId { get; set; }
    public string Status { get; set; } = string.Empty;
}