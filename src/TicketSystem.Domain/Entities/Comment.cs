using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Domain.Entities;

public class Comment
{
    private Comment()
    {
    }

    public Guid Id { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public Guid TicketId { get; private set; }
    public Guid AuthorId { get; private set; }


    public static Comment Create(string content, Guid ticketId, Guid authorId)
    {
        ValidateContent(content);
        return new Comment
        {
            Id = Guid.NewGuid(),
            Content = content,
            CreatedAt = DateTime.UtcNow,
            TicketId = ticketId,
            AuthorId = authorId
        };
    }


    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content)) throw new DomainException("can't create an empty comment");
        if (content.Length > 240) throw new DomainException("comment can't be longer than 240 characters");
    }
}