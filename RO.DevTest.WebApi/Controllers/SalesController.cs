using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;
using RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;
using RO.DevTest.Application.Features.Sales.Commands.DeleteSaleCommand;
using RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;
using RO.DevTest.Application.Features.Sales.Queries.GetSaleByIdQuery;
using RO.DevTest.Application.Features.Sales.Queries.GetSalesByPeriodQuery;
using RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;

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

    [HttpGet("analytics")]
    [ProducesResponseType(typeof(GetSalesByPeriodResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetSalesByPeriodResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSalesByPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _mediator.Send(new GetSalesByPeriodQuery(startDate,endDate));
        return Ok(result);
    }

    [HttpGet("paginated")]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPaginatedSales([FromQuery] GetSalesQuery query)
    {
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetSaleByIdResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetSaleByIdResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById([FromRoute] string id)
    {
        var result = await _mediator.Send(new GetSaleByIdQuery(id));
        if (result == null) return NotFound($"Sale with ID {id} not found");
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(CreateSaleResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateSale([FromForm] UpdateSaleCommand sale, string id)
    {
        if (id != sale.Id) return BadRequest("ID URL does not match ID from the sale");
        
        await _mediator.Send(sale);

        return Accepted(HttpContext.Request.GetDisplayUrl(), sale);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(string id)
    {
        var result = await _mediator.Send(new DeleteSaleCommand(id));
        if (!result) return NotFound($"Sale with ID {id} not found");
        return NoContent();
    }
}