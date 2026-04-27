namespace TicketSystem.Application.Comments.Commands.AddComment;

public class AddCommentCommand
{
    public Guid AuthorId { get; set; }
    public Guid TicketId { get; set; }
    public string Content { get; set; } = string.Empty;
}