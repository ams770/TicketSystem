using TicketSystem.Application.Tickets.Queries.GetTicketById;

namespace TicketSystem.Application.Tickets.Queries.Dtos;

public class TicketDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Status { get; init; } = null!;
    public string Priority { get; init; } = null!;
    public string Category { get; init; } = null!;
    public string CreatedBy { get; init; } = null!; // User's full name
    public TicketAgentDto? AssignedTo { get; init; } // agent's full name
    public DateTime CreatedAt { get; init; }
    public DateTime? ResolvedAt { get; init; }
    public ICollection<TicketCommentDto> Comments { get; init; } = [];
}