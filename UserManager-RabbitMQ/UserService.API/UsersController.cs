using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Users.Commands;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string name, string email)
    {
        await _mediator.Send(new RegisterUserCommand(name, email));
        return Ok("User registration initiated.");
    }
}
