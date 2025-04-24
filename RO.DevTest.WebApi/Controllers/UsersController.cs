using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand;
using RO.DevTest.Application.Features.User.Commands.DeleteUserCommand;
using RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;
using RO.DevTest.Application.Features.User.Queries.GetUserByIdQuery;
using RO.DevTest.Application.Features.User.Queries.GetUsersQuery;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/user")]
[OpenApiTags("Users")]
public class UsersController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateUserResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand request) {
        CreateUserResult response = await _mediator.Send(request);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUsersQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(string id)
    {
        var request = new GetUserByIdQuery(id);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(GetUserByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand request)
    {
        if (id != request.UserId) return BadRequest("User ID mismatch");
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var request = new DeleteUserCommand(id);
        await _mediator.Send(request);
        return NoContent();
    }
}
