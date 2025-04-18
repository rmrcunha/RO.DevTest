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
    public string ProductDescription { get; set; } = string.Empty;

    public CreateProductResult() { }

    public CreateProductResult(Domain.Entities.Product product)
    {
        ProductId = product.Id;
        ProductName = product.Name;
        ProductDescription = product.Description;
    }
}
