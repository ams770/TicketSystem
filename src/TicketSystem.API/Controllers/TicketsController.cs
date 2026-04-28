using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketSystem.Application.Tickets.Commands.AssignTicket;
using TicketSystem.Application.Tickets.Commands.ChangeTicketStatus;
using TicketSystem.Application.Tickets.Commands.CreateTicket;
using TicketSystem.Application.Tickets.Queries.GetAllTickets;
using TicketSystem.Application.Tickets.Queries.GetTicketById;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TicketsController(
    CreateTicketService createTicket,
    AssignTicketService assignTicket,
    ChangeTicketStatusService changeStatus,
    GetTicketByIdService getById,
    GetAllTicketsService getAll)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTicketsQuery query)
    {
        var result = await getAll.ExecuteAsync(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await getById.ExecuteAsync(new GetTicketByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
    {

        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await createTicket.ExecuteAsync(userId, command);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPost("assign")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> Assign([FromBody] AssignTicketCommand command)
    {
        var result = await assignTicket.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPatch("status")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeTicketStatusCommand command)
    {
        var result = await changeStatus.ExecuteAsync(command);
        return Ok(result);
    }
}