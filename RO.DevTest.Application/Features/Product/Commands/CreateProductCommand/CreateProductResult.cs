using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public record CreateProductResult
{
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public double ProductPrice { get; set; } = 0.0;
    public int ProductQuantity { get; set; } = 0;
    public string ProductDescription { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public CreateProductResult() { }

    public CreateProductResult(Domain.Entities.Product product)
    {
        ProductId = product.Id;
        ProductName = product.Name;
        ProductPrice = product.Price;
        ProductDescription = product.Description;
        ProductQuantity = product.Quantity;
        CreatedAt = product.CreatedAt;
        UpdatedAt = product.UpdatedAt;
    }
}
