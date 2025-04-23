using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManager.Application.Common.Attributes;
using UserManager.Application.Users.Commands;
using UserManager.Application.Users.Queries;

namespace UserManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] string name)
    {
        var id = await _mediator.Send(new CreateUserCommand(name));
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet("all")]
    [RequiresAdmin]
    public IActionResult GetAllUsers()
    {
        return Ok(new[] { "AdminUser1", "AdminUser2" });
    }
}
