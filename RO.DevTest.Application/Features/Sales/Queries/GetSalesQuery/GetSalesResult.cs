using RO.DevTest.Application.Features.Sales.DTO;

namespace RO.DevTest.Application.Features.Sales.Queries.GetSalesQuery;

public record GetSalesResult
{
    public IEnumerable<Domain.Entities.Sale> Items { get; set; } =[];
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}