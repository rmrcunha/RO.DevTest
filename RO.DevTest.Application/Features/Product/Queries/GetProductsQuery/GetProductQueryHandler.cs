using MediatR;
using Microsoft.EntityFrameworkCore;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductQueryHandler(IProductsRepository productsRepository, bool test = false) : IRequestHandler<GetProductsQuery, GetProductsQueryResult>
{

    public async Task<GetProductsQueryResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // Get the products from the repository
        var query = productsRepository.Query();

        if (!string.IsNullOrEmpty(request.SearchTerm)) query = query.Where(x => x.Name.ToLower().Contains(request.SearchTerm.ToLower()));

        if ((request.SortBy?.ToLower() == "price")) query = request.IsAscending ? query.OrderBy(x => x.Price) : query.OrderByDescending(x => x.Price);

        else if ((request.SortBy?.ToLower() == "name")) query = request.IsAscending ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);

        var totalCount = query.Count();

        List<Domain.Entities.Product> items;

        if (test) items = query.Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToList();
       
        else items = await query.Skip((request.Page - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken);

        return new GetProductsQueryResult
        {
            Items = items,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling((double)totalCount / request.PageSize)
        };
    }
}
