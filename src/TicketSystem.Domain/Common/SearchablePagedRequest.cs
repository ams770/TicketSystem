namespace TicketSystem.Domain.Common;

public class SearchablePagedRequest : PagedRequest
{
    public string? SearchTerm { get; set; }
}