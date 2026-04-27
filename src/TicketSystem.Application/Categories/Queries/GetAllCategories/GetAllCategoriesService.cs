using TicketSystem.Application.Tickets.Queries.Dtos;
using TicketSystem.Domain.Common;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Interfaces;

namespace TicketSystem.Application.Tickets.Queries.
;

public class GetAllCategoriesService(ITicketRepo ticketRepo)
{
    public async Task<PagedResult<TicketDto>> ExecuteAsync(GetAllTicketsQuery query)
    {
        // Fetch paged result
        var pagedTickets = await ticketRepo.GetAllAsync(query);
        // Map to Dto 
        var dtoItems = pagedTickets.Items
            .Select(item => item.ToDto())
            .ToList();


        return new PagedResult<TicketDto>
        {
            Items = dtoItems,
            TotalCount = pagedTickets.TotalCount,
            Page = pagedTickets.Page,
            PageSize = pagedTickets.PageSize
        };
    }
}