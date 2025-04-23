using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;
using RO.DevTest.Application.Features.Auth.Commands.RegisterCommand;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
[OpenApiTags("Auth")]
public class AuthController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    [HttpPost("register")]
    [ProducesResponseType(typeof(RegisterResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(RegisterResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterCommand registerCommand)
    {
        var res = await _mediator.Send(registerCommand);
        if (res == null) throw new BadHttpRequestException("Error creating user");
        return Created(string.Empty, res);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginCommand loginCommand)
    {
        var res = await _mediator.Send(loginCommand);
        if (res == null) throw new BadHttpRequestException("Error logging in");
        return Ok(res);
    }
}
