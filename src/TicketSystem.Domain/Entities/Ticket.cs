using TicketSystem.Domain.Enums;
using TicketSystem.Domain.Exceptions;
using static TicketSystem.Domain.Enums.TicketStatus;

namespace TicketSystem.Domain.Entities;

public class Ticket
{
    private Ticket()
    {
    }

    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public TicketStatus Status { get; private set; }
    public TicketPriority Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }

    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid? AgentId { get; private set; }
    public Agent? Agent { get; private set; }

    private readonly List<Comment> _comments = [];
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private ICollection<TicketStatus> AllowedStatusChange()
    {
        return Status switch
        {
            Open => [InProgress],
            InProgress => [Open, Resolved],
            Resolved => [Open, InProgress, Closed],
            _ => Array.Empty<TicketStatus>()
        };
    }


    public static Ticket Create(string title,
        string description, Guid categoryId, Guid userId, TicketPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Ticket title is required.");

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Ticket description is required.");


        return new Ticket
        {
            Id = Guid.NewGuid(),
            Title = title.Trim(),
            Description = description.Trim(),
            Status = Open,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CategoryId = categoryId,
            UserId = userId,
            Priority = priority,
        };
    }


    public void ChangeStatus(TicketStatus newStatus)
    {
        ValidateTicketEditable();
        var allowedStatusChange = AllowedStatusChange();
        if (!allowedStatusChange.Contains(newStatus)) throw new DomainException("Invalid status change.");

        if (newStatus == Resolved) ResolvedAt = DateTime.UtcNow;

        UpdatedAt = DateTime.UtcNow;
        Status = newStatus;
    }


    public void AddComment(Comment comment)
    {
        ValidateTicketEditable();
        _comments.Add(comment);
    }

    public void AssignAgent(Agent agent)
    {
        ValidateTicketEditable();
        if (!agent.IsAvailable) throw new DomainException("Agent is not available.");

        AgentId = agent.Id;
        Agent = agent;
        UpdatedAt = DateTime.UtcNow;
        Status = InProgress;
    }


    private void ValidateTicketEditable()
    {
        if (Status == Closed)
            throw new DomainException("Cannot update a closed ticket.");
    }
}