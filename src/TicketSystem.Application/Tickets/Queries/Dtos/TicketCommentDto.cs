namespace TicketSystem.Application.Tickets.Queries.Dtos;

public class TicketCommentDto
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public Guid AuthorId { get; init; }
    public DateTime CreatedAt { get; init; }
}