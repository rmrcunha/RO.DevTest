namespace RO.DevTest.Application.Features.Sales.Queries.GetSaleByIdQuery;

public class GetSaleByIdResult
{
    public string Id { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
}