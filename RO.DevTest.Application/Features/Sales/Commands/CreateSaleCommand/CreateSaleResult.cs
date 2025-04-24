using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public class CreateSaleResult
{
    public string SaleId { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public Domain.Entities.Product? Product { get; set; }
    public int Quantity { get; set; } = 0;
    public double TotalPrice { get; set; } = 0.0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public CreateSaleResult(Domain.Entities.Sale sale) 
    {
        SaleId = sale.Id;
        ProductId = sale.ProductId;
        Product = sale.Product;
        Quantity = sale.Quantity;
        TotalPrice = sale.TotalPrice;
        CreatedAt = sale.CreatedAt;
    }
}
