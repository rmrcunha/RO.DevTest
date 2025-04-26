using RO.DevTest.Application.Features.Sales.DTO;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;

public record GetSalesResult
{
    /// <summary>
    /// List of sales items.
    /// </summary>
    public IEnumerable<Domain.Entities.Sale> Items { get; set; } =[];
    /// <summary>
    /// Total number of items in the result set.
    /// </summary>
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}