namespace TicketSystem.Application.Tickets.Commands.CreateTicket;

public class CreateTicketResult
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}