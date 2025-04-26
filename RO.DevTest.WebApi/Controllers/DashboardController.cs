using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Dashboard.Queries;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/dashboard")]
[OpenApiTags("dashboard")]
[Authorize(Roles = "Admin")]
public class DashboardController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("summary")]
    [ProducesResponseType(typeof(DashboardSummaryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDashboardSummary(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetDashboardSummary());
        return Ok(result);
    }
}