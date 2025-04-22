using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler(IProductsRepository productsRepository) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productsRepository.GetByIdAsync(request.Id, cancellationToken);

        if (product == null) throw new Exception($"Product with ID {request.Id} not found");

        return new GetProductByIdResult(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Quantity,
            product.CreatedAt.ToString(),
            product.UpdatedAt.ToString()
        );
    }

}

