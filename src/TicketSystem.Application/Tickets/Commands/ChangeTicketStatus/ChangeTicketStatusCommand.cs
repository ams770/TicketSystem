using TicketSystem.Domain.Enums;

namespace TicketSystem.Application.Tickets.Commands.ChangeTicketStatus;

public class ChangeTicketStatusCommand
{
    public Guid TicketId { get; set; }
    public TicketStatus Status { get; set; }
}