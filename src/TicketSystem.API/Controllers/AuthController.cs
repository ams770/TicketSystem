using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Auth.Commands.LoginAgent;
using TicketSystem.Application.Auth.Commands.LoginUser;
using TicketSystem.Application.Auth.Commands.RegisterAgent;
using TicketSystem.Application.Auth.Commands.RegisterUser;

namespace TicketSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController(
    RegisterUserService registerUser,
    RegisterAgentService registerAgent,
    LoginUserService loginUser,
    LoginAgentService loginAgent)
    : ControllerBase
{
    [HttpPost("register/user")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand command)
    {
        var result = await registerUser.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPost("register/agent")]
    public async Task<IActionResult> RegisterAgent([FromBody] RegisterAgentCommand command)
    {
        var result = await registerAgent.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPost("login/user")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand command)
    {
        var result = await loginUser.ExecuteAsync(command);
        return Ok(result);
    }

    [HttpPost("login/agent")]
    public async Task<IActionResult> LoginAgent([FromBody] LoginAgentCommand command)
    {
        var result = await loginAgent.ExecuteAsync(command);
        return Ok(result);
    }
}