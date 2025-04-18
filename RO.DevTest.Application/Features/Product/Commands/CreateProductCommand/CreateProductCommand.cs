using MediatR;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public record CreateProductCommand : IRequest<CreateProductResult>{
    public string Name { get; set; } = string.Empty ;
    public string Description { get; set; } = string.Empty;

    public Domain.Entities.Product AssignTo()
    {
        return new Domain.Entities.Product
        {
            Name = Name,
            Description = Description
        };
    }

}