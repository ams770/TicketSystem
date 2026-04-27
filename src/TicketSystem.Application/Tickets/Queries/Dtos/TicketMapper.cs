using TicketSystem.Domain.Entities;

namespace TicketSystem.Application.Tickets.Queries.Dtos;

public static class TicketMapper
{
    public static TicketDto ToDto(this Ticket ticket) => new()
    {
        Id = ticket.Id,
        Title = ticket.Title,
        Status = ticket.Status.ToString(),
        Priority = ticket.Priority.ToString(),
        Category = ticket.Category.Name,
        CreatedBy = ticket.User.FullName,
        AssignedTo = ticket.Agent is null
            ? null
            : new TicketAgentDto
            {
                Id = ticket.Agent.Id,
                FullName = ticket.Agent.FullName,
            },
        Comments = ticket.Comments
            .Select(c => new TicketCommentDto
            {
                Id = c.Id,
                Content = c.Content,
                AuthorId = c.AuthorId,
                CreatedAt = c.CreatedAt
            }).ToList(),
        CreatedAt = ticket.CreatedAt,
        ResolvedAt = ticket.ResolvedAt
    };
}