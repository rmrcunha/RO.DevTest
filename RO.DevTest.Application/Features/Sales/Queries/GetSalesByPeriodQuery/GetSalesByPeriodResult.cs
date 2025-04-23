using RO.DevTest.Application.Features.Sales.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesByPeriodQuery;

/// <summary>
/// Represents the result of a query to get sales by period.
/// </summary>
public record GetSalesByPeriodResult
{
    public int TotalSales { get; init; }
    public double TotalRevenue { get; init; }
    public IEnumerable<ProductRevenueDTO> ProductsRevenue { get; init; } = Enumerable.Empty<ProductRevenueDTO>();
}
