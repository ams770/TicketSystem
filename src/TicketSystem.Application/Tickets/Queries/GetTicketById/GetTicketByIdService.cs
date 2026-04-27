using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Tickets.Queries.Dtos;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Queries.GetTicketById;

public class GetTicketByIdService(ITicketRepo ticketRepo)
{
    public async Task<TicketDto> ExecuteAsync(GetTicketByIdQuery query)
    {
        var ticket = await ticketRepo.GetByIdAsync(query.TicketId) ??
                     throw new NotFoundException(nameof(Ticket), query.TicketId);
        return ticket.ToDto();
    }
}