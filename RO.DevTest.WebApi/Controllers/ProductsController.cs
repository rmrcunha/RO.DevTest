using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;
using RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/product")]
[OpenApiTags("Products")]

public class ProductsController(IMediator mediator):Controller
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateProductResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(CreateProductCommand product)
    {
        CreateProductResult response = await _mediator.Send(product);

        return Created(HttpContext.Request.GetDisplayUrl(), response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetProductsQueryResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery query)
    {
        var response = await _mediator.Send(query);

        return Ok(response);
    }

}