# Domain Layer Architecture - TicketSystem

The Domain layer is the heart of the TicketSystem application, containing the core business logic, entities, and rules. It is designed to be independent of external frameworks, databases, and UI concerns.

## Directory Structure

- **Entities/**: Contains the core domain models. These are rich entities that encapsulate both state and behavior.
- **Interfaces/**: Defines the repository interfaces and other abstractions that the application layer uses to interact with the domain.
- **Enums/**: Contains domain-specific enumerations (e.g., TicketStatus, TicketPriority).
- **Exceptions/**: Custom exceptions specific to domain rule violations (e.g., `DomainException`).
- **Common/**: Shared domain-level models, such as request/result objects for pagination.

## File Structure

```text
TicketSystem.Domain/
├── Common/
│   ├── PagedRequest.cs
│   ├── PagedResult.cs
│   ├── SearchablePagedRequest.cs
│   └── TicketPagedRequest.cs
├── Entities/
│   ├── Agent.cs
│   ├── BaseActor.cs
│   ├── Category.cs
│   ├── Comment.cs
│   ├── Ticket.cs
│   └── User.cs
├── Enums/
│   ├── TicketPriority.cs
│   └── TicketStatus.cs
├── Exceptions/
│   └── DomainException.cs
├── Interfaces/
│   ├── IAgentRepo.cs
│   ├── ICategoryRepo.cs
│   ├── IDomainRepo.cs
│   ├── ITicketRepo.cs
│   └── IUserRepo.cs
├── Architecture.md
└── TicketSystem.Domain.csproj
```


## Core Design Principles

### 1. Rich Domain Models (DDD)
Entities like `Ticket` are not just "bags of data". They contain logic to maintain their own invariants:
- **Encapsulation**: Private setters ensure that the state can only be modified through controlled methods.
- **State Transitions**: The `Ticket` entity manages its own status transitions via `AllowedStatusChange()` and `ChangeStatus()`.
- **Validation**: Domain rules are enforced within the entity (e.g., ensuring a title is not empty, preventing updates to closed tickets).

### 2. Repository Pattern (Abstractions)
The domain defines interfaces (e.g., `ITicketRepo`, `IUserRepo`) that represent how data should be accessed. The actual implementation of these interfaces resides in the Infrastructure layer, keeping the Domain layer pure.

### 3. Domain Exceptions
Business rule violations throw `DomainException`, which can be caught and handled at the Application or API level to provide meaningful feedback to the user without exposing technical details.

## Key Entities

| Entity | Description |
| :--- | :--- |
| **Ticket** | The central entity representing a support request. Manages its own status, priority, category, and assignments. |
| **Agent** | Represents a support staff member who can be assigned to tickets. |
| **User** | Represents the customer or requester who creates tickets. |
| **Category** | Categorizes tickets for better organization and routing. |
| **Comment** | Represents communication between users and agents on a specific ticket. |

## Workflow Example: Ticket Status Change

1. A request comes in to change a ticket's status.
2. The domain entity `Ticket` validates if the current status allows moving to the new status.
3. If valid, the state is updated along with the `UpdatedAt` timestamp.
4. If invalid, a `DomainException` is thrown, preventing the system from entering an inconsistent state.
