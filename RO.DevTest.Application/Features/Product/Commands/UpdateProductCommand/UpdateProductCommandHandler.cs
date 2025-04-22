using MediatR;
using RO.DevTest.Application.Contracts.Persistance.Repositories;
using System;
using System.Collections.Generic;
using FluentValidation.Results;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.Product.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductsRepository _productsRepository = productsRepository;
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product == null) throw new KeyNotFoundException("Product not found");

        UpdateProductCommandValidator productValidator = new();

        ValidationResult validationResult = await productValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid) throw new BadRequestException(validationResult);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Quantity = request.Quantity;
        product.UpdatedAt = request.UpdatedAt;

        await _productsRepository.UpdateAsync(product, cancellationToken);

        if(product == null) throw new BadRequestException("Product update failed");

        return new UpdateProductResult(product);
    }
}
