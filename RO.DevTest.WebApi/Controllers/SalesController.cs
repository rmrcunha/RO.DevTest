using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

namespace RO.DevTest.WebApi.Controllers;
[ApiController]
[Route("api/sales")]
[OpenApiTags("Sales")]
public class SalesController(IMediator mediator):Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSale([FromForm] CreateSaleCommand sale)
    {
        CreateSaleResult response = await _mediator.Send(sale);
        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }
}