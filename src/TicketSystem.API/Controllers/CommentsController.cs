using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketSystem.Application.Comments.Commands.AddComment;

namespace TicketSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentsController(AddCommentService addComment) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCommentCommand command)
    {
        var authorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await addComment.ExecuteAsync(authorId, command);
        return CreatedAtAction(nameof(Add), result);
    }
}