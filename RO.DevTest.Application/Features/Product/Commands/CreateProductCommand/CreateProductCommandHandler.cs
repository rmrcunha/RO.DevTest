using MediatR;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Exception;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Persistance.Repositories;

namespace RO.DevTest.Application.Features.Product.Commands.CreateProductCommand;

public class CreateProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        CreateProductCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }
        Domain.Entities.Product newProduct = request.AssignTo();
        await _productsRepository.CreateAsync(newProduct);
        if (newProduct == null)
        {
            throw new BadRequestException("Product creation failed");
        }


        return new CreateProductResult(newProduct);
    }
}
