namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

public class UpdateSaleResult
(
    string id,
    string productId,
    int quantity,
    double totalPrice,
    DateTime UpdatedAt
    )
{
    public string Id { get; } = id;
    public string ProductId { get; } = productId;
    public int Quantity { get; } = quantity;
    public double TotalPrice { get; } = totalPrice;
    public DateTime UpdatedAt { get; } = UpdatedAt;
};