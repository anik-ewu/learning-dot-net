using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaUserDemo.API.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KafkaUserDemo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        await _mediator.Send(command);
        return Ok("User resigtraation request publish");
    }
}
