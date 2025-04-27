using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleByIdQuery;

public class GetSaleByIdQueryHandler(ISalesRepository salesRepository): IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
{
    public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var sale = await salesRepository.GetByIdAsync(request.id, cancellationToken);
        if (sale == null) throw new Exception($"Sale with id {request.id} not found");
        return new GetSaleByIdResult
        {
            Id = sale.Id,
            ProductId = sale.ProductId,
            Quantity = sale.Quantity,
            TotalPrice = sale.TotalPrice,
            CreatedAt = sale.CreatedAt
        };
    }
}