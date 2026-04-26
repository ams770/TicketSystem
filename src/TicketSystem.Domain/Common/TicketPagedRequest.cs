using TicketSystem.Domain.Enums;

namespace TicketSystem.Domain.Common;

public class TicketPagedRequest : SearchablePagedRequest
{
    public TicketStatus? Status { get; set; }
    public TicketPriority? Priority { get; set; }
    public Guid? UserId { get; set; }
    public Guid? AgentId { get; set; }
    public Guid? CategoryId { get; set; }
}