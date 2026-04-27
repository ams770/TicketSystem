using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Comments.Commands.AddComment;

public class AddCommentService(ITicketRepo ticketRepo)
{
    public async Task<AddCommentResult> ExecuteAsync(AddCommentCommand command)
    {
        var ticket = await ticketRepo.GetByIdAsync(command.TicketId) ??
                     throw new NotFoundException(nameof(Ticket), command.TicketId);
        // -
        var commentCreated = Comment.Create(command.Content, command.TicketId, command.AuthorId);
        ticket.AddComment(commentCreated);
        await ticketRepo.AddCommentAsync(commentCreated); 
        await ticketRepo.SaveChangesAsync();
        // -

        return new AddCommentResult
        {
            Id = commentCreated.Id,
            CreatedAt =  commentCreated.CreatedAt,
        };
    }
}