using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.CreateSaleCommand;

public record CreateSaleCommand:IRequest<CreateSaleResult>
{
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; } = 0;
    public double TotalPrice { get; set; } = 0.0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Domain.Entities.Sale AssignTo()
    {
        return new Domain.Entities.Sale
        {
            ProductId = ProductId,
            Quantity = Quantity,
            TotalPrice = TotalPrice,
            CreatedAt = CreatedAt.ToUniversalTime(),
        };
    }
}
