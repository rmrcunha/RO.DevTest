using MediatR;
using RO.DevTest.Application.Features.Product.Queries.GetProductByIdQuery;
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
    public Domain.Entities.Sale AssignTo()
    {
        return new Domain.Entities.Sale
        {
            ProductId = ProductId,
            Quantity = Quantity,
        };
    }
}
