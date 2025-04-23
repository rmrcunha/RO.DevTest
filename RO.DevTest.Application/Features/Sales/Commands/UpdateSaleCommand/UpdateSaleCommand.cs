using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Sales.Commands.UpdateSaleCommand;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public string Id { get; init; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public DateTime SaleDate { get; set; }

    public Domain.Entities.Sale AssignTo()
    {
        return new Domain.Entities.Sale
        {
            Id = Id,
            ProductId = ProductId,
            Quantity = Quantity,
            TotalPrice = Price,
            CreatedAt = SaleDate
        };
    }

   
}

