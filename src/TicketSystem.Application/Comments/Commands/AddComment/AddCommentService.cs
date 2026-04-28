using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Interfaces;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Comments.Commands.AddComment;

public class AddCommentService(IUnitOfWork unitOfWork, ITicketRepo ticketRepo)
{
    public async Task<AddCommentResult> ExecuteAsync(Guid authorId, AddCommentCommand command)
    {
        var ticket = await ticketRepo.GetByIdAsync(command.TicketId) ??
                     throw new NotFoundException(nameof(Ticket), command.TicketId);
        // -
        var commentCreated = Comment.Create(command.Content, command.TicketId, authorId);
        ticket.AddComment(commentCreated);
        await ticketRepo.AddCommentAsync(commentCreated);
        await unitOfWork.SaveChangesAsync();
        // -

        return new AddCommentResult
        {
            Id = commentCreated.Id,
            CreatedAt = commentCreated.CreatedAt,
        };
    }
}