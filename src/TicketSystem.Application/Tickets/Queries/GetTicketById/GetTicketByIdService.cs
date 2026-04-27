using TicketSystem.Application.Common.Exceptions;
using TicketSystem.Application.Tickets.Queries.Dtos;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Queries.GetTicketById;

public class GetTicketByIdService(ITicketRepo ticketRepo)
{
    public async Task<TicketDto> ExecuteAsync(GetTicketByIdQuery query)
    {
        var ticket = await ticketRepo.GetByIdAsync(query.Id) ??
                     throw new NotFoundException(nameof(Ticket), query.Id);
        return ticket.ToDto();
    }
}