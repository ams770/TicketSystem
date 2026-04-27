using System.ComponentModel.DataAnnotations;
using TicketSystem.Domain.Enums;

namespace TicketSystem.Application.Tickets.Commands.CreateTicket;

public class CreateTicketCommand
{
    public Guid UserId { get; set; } // Temporary Until Authentication Integrated
    public Guid CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; }
}