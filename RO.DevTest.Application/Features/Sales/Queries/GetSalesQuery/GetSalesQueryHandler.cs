using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;
using RO.DevTest.Application.Features.Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;

public class GetSalesQueryHandler(ISalesRepository salesRepository): IRequestHandler<GetSalesQuery, GetSalesResult>
{
    public async Task<GetSalesResult> Handle(GetSalesQuery request, CancellationToken cancellationToken)
    {
        var query = salesRepository.Query();
        if(request.SortBy?.ToLower() == "quantity") query = request.SortDescending ? query.OrderByDescending(x => x.Quantity) : query.OrderBy(x => x.Quantity);
        else if (request.SortBy?.ToLower() == "price") query = request.SortDescending ? query.OrderByDescending(x => x.TotalPrice) : query.OrderBy(x => x.TotalPrice);
        else if (request.SortBy?.ToLower() == "date") query = request.SortDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new GetSalesResult
        {
            Items = items,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };
    }
}
