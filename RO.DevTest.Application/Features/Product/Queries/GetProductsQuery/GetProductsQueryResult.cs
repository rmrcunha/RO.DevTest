using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RO.DevTest.Application.Features.Product.Queries.GetProductsQuery;

public class GetProductsQueryResult
{
    public IEnumerable<Domain.Entities.Product> Items{get; set; } = Enumerable.Empty<Domain.Entities.Product>();
    public int TotalCount { get; set; } = 0;
    public int TotalPages { get; set; } = 1;
}
