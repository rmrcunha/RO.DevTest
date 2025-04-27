using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Commands.DeleteProductCommand;
using RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;
using RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

namespace RO.DevTest.WebApi.Controllers;

[ApiController]
[Route("api/products")]
[OpenApiTags("Products")]


public class ProductsController(IMediator mediator):Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromForm]CreateProductCommand product)
    {
        CreateProductResult response = await _mediator.Send(product);

        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        var response = await _mediator.Send(query);

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductById(string id)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id));
        if (response == null) return NotFound($"Product with ID {id} not found");
        return Ok(response);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProductAsync([FromForm] UpdateProductCommand product, string id)
    {
        if (id != product.Id) return BadRequest("ID URL does not match ID from the product");
        product.Id = id;
        await _mediator.Send(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UpdateProductResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(string id)
    {
        var response = await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }

}