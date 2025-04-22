using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest<UpdateProductResult>
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string UpdatedAt { get; set; } = DateTime.UtcNow.ToString();

    public Domain.Entities.Product AssignTo()
    {
        return new Domain.Entities.Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            Quantity = Quantity,
            UpdatedAt = UpdatedAt,
        };
    }
   
}
