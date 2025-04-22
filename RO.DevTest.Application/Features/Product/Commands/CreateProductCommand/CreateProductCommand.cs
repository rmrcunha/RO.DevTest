using MediatR;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public record CreateProductCommand : IRequest<CreateProductResult>{
    public string Name { get; set; } = string.Empty ;
    public double Price { get; set; } = 0.0;
    public int Quantity { get; set; } = 0;
    public string Description { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = DateTime.UtcNow.ToString();
    public string UpdatedAt { get; set; } = DateTime.UtcNow.ToString();


    public Domain.Entities.Product AssignTo()
    {
        return new Domain.Entities.Product
        {
            Name = Name,
            Price = Price,
            Description = Description,
            Quantity = Quantity,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }

}