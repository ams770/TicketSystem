```text
+---------------------------------------+
|                 API                   |  ← HTTP, Controllers, Middleware
+---------------------------------------+
|            Infrastructure             |  ← EF Core, JWT, BCrypt, Repos
+---------------------------------------+
|             Application               |  ← Use Cases, Commands, Queries
+---------------------------------------+
|                Domain                 |  ← Entities, Business Rules, Interfaces
+---------------------------------------+

TicketSystem/
└── src/
    ├── TicketSystem.Domain/
    │   ├── Common/              # PagedRequest, PagedResult, TicketPagedRequest
    │   ├── Entities/            # Ticket, User, Agent, Comment, Category, BaseActor
    │   ├── Enums/               # TicketStatus, TicketPriority
    │   ├── Exceptions/          # DomainException
    │   └── Interfaces/          # ITicketRepo, IUserRepo, IAgentRepo, ICategoryRepo
    ├── TicketSystem.Application/
    │   ├── Auth/
    │   │   └── Commands/        # RegisterUser, RegisterAgent, LoginUser, LoginAgent
    │   ├── Tickets/
    │   │   ├── Commands/        # CreateTicket, AssignTicket, ChangeTicketStatus
    │   │   └── Queries/         # GetTicketById, GetAllTickets, Dtos, TicketMapper
    │   ├── Comments/
    │   │   └── Commands/        # AddComment
    │   ├── Common/
    │   │   └── Exceptions/      # NotFoundException, ValidationException, ConflictException
    │   └── Interfaces/          # IJwtService, IPasswordHasher
    ├── TicketSystem.Infrastructure/
    │   ├── Persistence/
    │   │   ├── AppDbContext.cs
    │   │   ├── Configurations/  # EF Core entity configurations
    │   │   └── Repositories/    # TicketRepo, UserRepo, AgentRepo, CategoryRepo
    │   └── Services/            # JwtService, PasswordHasher
    ├── TicketSystem.API/
    │   ├── Controllers/         # AuthController, TicketsController, CommentsController
    │   └── Middleware/          # ExceptionHandlingMiddleware
    └── TicketSystem.sln
